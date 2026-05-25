using Dapper;
using SchoolNotificationAPI.Application.Feature.Notifications.DTOs;
using SchoolNotificationAPI.Application.Feature.Notifications.Interfaces;
using SchoolNotificationAPI.Domain.Entities;
using SchoolNotificationAPI.Infrastructure.Persistence;

namespace SchoolNotificationAPI.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DapperContext _context;

        public NotificationRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Notification notification)
        {
            using var connection = _context.CreateConnection();

            await connection.OpenAsync();

            var sql = """
            INSERT INTO notifications
            (
                id,
                type,
                title,
                content,
                image_url,
                created_at
            )
            VALUES
            (
                @Id,
                @Type,
                @Title,
                @Content,
                @ImageUrl,
                @CreatedAt
            );
            """;

            await connection.ExecuteAsync(sql, notification);
        }

        public async Task CreateRecipientsAsync(
            IEnumerable<NotificationRecipient> recipients)
        {
            using var connection = _context.CreateConnection();

            await connection.OpenAsync();

            var sql = """
            INSERT INTO notification_recipients
            (
                id,
                notification_id,
                student_id,
                contact_phone
            )
            VALUES
            (
                @Id,
                @NotificationId,
                @StudentId,
                @ContactPhone
            );
            """;

            await connection.ExecuteAsync(sql, recipients);
        }

        public async Task<IEnumerable<ParentNotificationDto>>
            GetByPhoneAsync(string phone)
        {
            using var connection = _context.CreateConnection();

            await connection.OpenAsync();

            var sql = """
            SELECT
                nr.id,
                n.type,
                n.title,
                n.content,
                n.image_url AS "ImageUrl",
                n.created_at AS "CreatedAt",
                nr.read_at AS "ReadAt"
            FROM notification_recipients nr
            INNER JOIN notifications n
                ON n.id = nr.notification_id
            WHERE nr.contact_phone = @Phone
            ORDER BY n.created_at DESC;
            """;

            return await connection.QueryAsync<ParentNotificationDto>(
                sql,
                new { Phone = phone });
        }

        public async Task MarkAsReadAsync(Guid recipientId)
        {
            using var connection = _context.CreateConnection();

            await connection.OpenAsync();

            var sql = """
            UPDATE notification_recipients
            SET read_at = NOW()
            WHERE id = @RecipientId;
            """;

            await connection.ExecuteAsync(
                sql,
                new { RecipientId = recipientId });
        }
    }
}
