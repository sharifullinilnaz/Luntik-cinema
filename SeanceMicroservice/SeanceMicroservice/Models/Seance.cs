using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeanceMicroservice.Models
{
    /// <summary>
    /// Сеанс.
    /// </summary>
    [Table("seances")]
    public class Seance
    {
        /// <summary>
        /// Идентификатор. Уникальный ключ.
        /// </summary>
        [Column("id", TypeName = "serial")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [Column("name", TypeName = "varchar(128)")]
        public string Name { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        [Column("date", TypeName = "varchar(32)")]
        public string Date { get; set; }

        /// <summary>
        /// Зал
        /// </summary>
        [Column("hall", TypeName = "integer")]
        public int Hall { get; set; }

        /// <summary>
        /// Фильм
        /// </summary>
        [Column("film_id", TypeName = "integer")]
        public int FilmId { get; set; }

    }
}
