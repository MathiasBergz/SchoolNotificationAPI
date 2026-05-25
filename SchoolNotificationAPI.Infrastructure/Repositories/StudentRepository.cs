using Dapper;
using SchoolNotificationAPI.Domain.Entities;
using SchoolNotificationAPI.Infrastructure.Persistence;
using SchoolNotificationAPI.Application.Feature.Students.Interfaces;

namespace SchoolNotificationAPI.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DapperContext _context;

        public StudentRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(Student student)
        {
            using var connection = _context.CreateConnection();

            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                var studentSql = """
                INSERT INTO students
                (
                    id,
                    name,
                    year,
                    group_class,
                    period,
                    created_at
                )
                VALUES
                (
                    @Id,
                    @Name,
                    @Year,
                    @GroupClass,
                    @Period,
                    @CreatedAt
                );
                """;

                await connection.ExecuteAsync(
                    studentSql,
                    new
                    {
                        student.Id,
                        student.Name,
                        student.Year,
                        student.GroupClass,
                        student.Period,
                        student.CreatedAt
                    },
                    transaction);

                var contactSql = """
                INSERT INTO student_contacts
                (
                    id,
                    student_id,
                    parent_name,
                    phone_number,
                    is_main_contact
                )
                VALUES
                (
                    @Id,
                    @StudentId,
                    @ParentName,
                    @PhoneNumber,
                    @IsMainContact
                );
                """;

                foreach (var contact in student.Contacts)
                {
                    await connection.ExecuteAsync(
                        contactSql,
                        new
                        {
                            contact.Id,
                            contact.StudentId,
                            contact.ParentName,
                            contact.PhoneNumber,
                            contact.IsMainContact
                        },
                        transaction);
                }

                transaction.Commit();

                return student.Id;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            using var connection = _context.CreateConnection();

            await connection.OpenAsync();

            var sql = """
            SELECT
                s.id,
                s.name,
                s.year,
                s.group_class AS "GroupClass",
                s.period,
                s.created_at AS "CreatedAt",
                sc.id,
                sc.student_id AS "StudentId",
                sc.parent_name AS "ParentName",
                sc.phone_number AS "PhoneNumber",
                sc.is_main_contact AS "IsMainContact"
            FROM students s
            LEFT JOIN student_contacts sc ON sc.student_id = s.id;
            """;

            var studentDict = new Dictionary<Guid, Student>();

            await connection.QueryAsync<Student, StudentContact, Student>(
                sql,
                (student, contact) =>
                {
                    if (!studentDict.TryGetValue(student.Id, out var existingStudent))
                    {
                        existingStudent = student;
                        studentDict[student.Id] = existingStudent;
                    }

                    if (contact is not null)
                        existingStudent.AddContact(contact);

                    return existingStudent;
                },
                splitOn: "id"
            );

            return studentDict.Values;
        }

        public async Task<Student?> GetByIdAsync(Guid id)
        {
            using var connection = _context.CreateConnection();

            await connection.OpenAsync();

            var sql = """
            SELECT
                id,
                name,
                year,
                group_class AS "GroupClass",
                period,
                created_at AS "CreatedAt"
            FROM students
            WHERE id = @Id;
            """;

            var student = await connection.QueryFirstOrDefaultAsync<Student>(
                sql,
                new { Id = id });

            if (student is null)
                return null;

            var contactsSql = """
            SELECT
                id,
                student_id AS "StudentId",
                parent_name AS "ParentName",
                phone_number AS "PhoneNumber",
                is_main_contact AS "IsMainContact"
            FROM student_contacts
            WHERE student_id = @Id;
            """;

            var contacts = await connection.QueryAsync<StudentContact>(
                contactsSql,
                new { Id = id });

            foreach (var contact in contacts)
            {
                student.AddContact(contact);
            }

            return student;
        }

        public async Task UpdateAsync(Student student)
        {
            using var connection = _context.CreateConnection();

            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                await connection.ExecuteAsync(
                    """
                    UPDATE students
                    SET
                        name = @Name,
                        year = @Year,
                        group_class = @GroupClass,
                        period = @Period
                    WHERE id = @Id;
                    """,
                    new
                    {
                        student.Id,
                        student.Name,
                        student.Year,
                        student.GroupClass,
                        student.Period
                    },
                    transaction);

                await connection.ExecuteAsync(
                    """
                    DELETE FROM student_contacts
                    WHERE student_id = @StudentId;
                    """,
                    new
                    {
                        StudentId = student.Id
                    },
                    transaction);

                var contactSql = """
                INSERT INTO student_contacts
                (
                    id,
                    student_id,
                    parent_name,
                    phone_number,
                    is_main_contact
                )
                VALUES
                (
                    @Id,
                    @StudentId,
                    @ParentName,
                    @PhoneNumber,
                    @IsMainContact
                );
                """;

                foreach (var contact in student.Contacts)
                {
                    await connection.ExecuteAsync(
                        contactSql,
                        new
                        {
                            contact.Id,
                            contact.StudentId,
                            contact.ParentName,
                            contact.PhoneNumber,
                            contact.IsMainContact
                        },
                        transaction);
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using var connection = _context.CreateConnection();

            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                await connection.ExecuteAsync(
                    """
                DELETE FROM student_contacts
                WHERE student_id = @Id;
                """,
                    new { Id = id },
                    transaction);

                await connection.ExecuteAsync(
                    """
                DELETE FROM students
                WHERE id = @Id;
                """,
                    new { Id = id },
                    transaction);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<IEnumerable<Student>> GetTargetsAsync(
        string period,
        List<string>? years,
        List<string>? groupClasses,
        List<Guid>? studentIds)
        {
            using var connection = _context.CreateConnection();

            await connection.OpenAsync();

            var sql = """
            SELECT
                id,
                name,
                year,
                group_class AS "GroupClass",
                period,
                created_at AS "CreatedAt"
            FROM students
            WHERE period = @Period
            """;

            if (years is not null && years.Any())
            {
                sql += " AND year = ANY(@Years)";
            }

            if (groupClasses is not null && groupClasses.Any())
            {
                sql += " AND group_class = ANY(@GroupClasses)";
            }

            if (studentIds is not null && studentIds.Any())
            {
                sql += " AND id = ANY(@StudentIds)";
            }

            var students = (
                await connection.QueryAsync<Student>(
                    sql,
                    new
                    {
                        Period = period,
                        Years = years?.ToArray(),
                        GroupClasses = groupClasses?.ToArray(),
                        StudentIds = studentIds?.ToArray()
                    }))
                .ToList();

            var contactsSql = """
            SELECT
                id,
                student_id AS "StudentId",
                parent_name AS "ParentName",
                phone_number AS "PhoneNumber",
                is_main_contact AS "IsMainContact"
            FROM student_contacts;
            """;

            var contacts = (
                await connection.QueryAsync<StudentContact>(
                    contactsSql))
                .ToList();

            foreach (var student in students)
            {
                var studentContacts = contacts
                    .Where(c => c.StudentId == student.Id);

                foreach (var contact in studentContacts)
                {
                    student.AddContact(contact);
                }
            }

            return students;
        }
    }
}
