using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmMicroservice.Models
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    [Table("films")]
    public class Film
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
        /// Описание
        /// </summary>
        [Column("description", TypeName = "varchar(2048)")]
        public string Description { get; set; }

        /// <summary>
        /// Возрастное ограничение
        /// </summary>
        [Column("age_limit", TypeName = "integer")]
        public int AgeLimit { get; set; }

        /// <summary>
        /// Длительность
        /// </summary>
        [Column("duration", TypeName = "integer")]
        public int Duration { get; set; }

        /// <summary>
        /// Дата премьеры
        /// </summary>
        [Column("premiere_date", TypeName = "varchar(10)")]
        public string PremiereDate { get; set; }

        /// <summary>
        /// Постер
        /// </summary>
        [Column("poster", TypeName = "varchar(256)")]
        public string Poster { get; set; }

    }
}
