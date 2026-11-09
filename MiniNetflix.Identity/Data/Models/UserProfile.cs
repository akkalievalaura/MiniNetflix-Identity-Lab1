using System.ComponentModel.DataAnnotations;

namespace MiniNetflix.Identity.Data.Models
{
    /// <summary>
    /// Профиль пользователя (сущность базы данных).
    /// </summary>
    public class UserProfile
    {
        /// <summary>
        /// Внутренний идентификатор (первичный ключ в БД).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Внутренний идентификатор пользователя (внешний ключ на <see cref="User"/>).
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Пользователь, которому принадлежит профиль.
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// Идентификатор аватара (GUID). Может быть пустым, если аватар не задан.
        /// </summary>
        public Guid? AvatarId { get; set; }

        /// <summary>
        /// ФИО пользователя. Может быть пустым, если не заполнено.
        /// </summary>
        [MaxLength(256)]
        public string? FullName { get; set; }

        /// <summary>
        /// Дата рождения. Может быть пустой, если не заполнена.
        /// </summary>
        public DateTime? BirthDate { get; set; }
    }
}
