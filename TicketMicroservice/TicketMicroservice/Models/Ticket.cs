using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketMicroservice.Models
{
    /// <summary>
    /// Билет.
    /// </summary>
    [Table("tickets")]
    public class Ticket
    {
        /// <summary>
        /// Идентификатор. Уникальный ключ.
        /// </summary>
        [Column("id", TypeName = "serial")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Место
        /// </summary>
        [Column("place", TypeName = "varchar(64)")]
        public string Place { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        [Column("price", TypeName = "integer")]
        public int Price { get; set; }

        /// <summary>
        /// Сеанс
        /// </summary>
        [Column("seance_id", TypeName = "integer")]
        public int SeanceId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        [Column("user_id", TypeName = "integer")]
        public int? UserId { get; set; }

    }
}
