using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AccountMicroservice.Models
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    [Table("users")]
    public class User
    {
        /// <summary>
        /// Идентификатор. Уникальный ключ.
        /// </summary>
        [Column("id", TypeName = "serial")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Почта
        /// </summary>
        [Column("email", TypeName = "varchar(128)")]
        public string Email { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Column("first_name", TypeName = "varchar(128)")]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Column("last_name", TypeName = "varchar(128)")]
        public string LastName { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [Column("date_of_birth", TypeName = "varchar(128)")]
        public string DateOfBirth { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [Column("phone_number", TypeName = "varchar(11)")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Хешированный пароль
        /// </summary>
        [Column("hashed_password", TypeName = "varchar(128)")]
        public string HashedPassword { get; set; }

        /// <summary>
        /// Является ли пользователь админом
        /// </summary>
        [Column("is_admin", TypeName = "boolean")]
        public bool IsAdmin { get; set; }

    }
}
