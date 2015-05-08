namespace Sample2015.Core.Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("acct_user")]
    public class AccountUser
    {
        public AccountUser()
        {
            this.DateCreated = DateTime.Now;
        }

        [Column("acct_user_id", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column("username", Order = 110)]
        [Index("ux_acct_user_username_companyid", 2, IsUnique = true)]
        [MaxLength(100)]
        [MinLength(3)]
        [Required]
        public string Username { get; set; }

        [Column("password", Order = 120)]
        [MaxLength(300)]
        [MinLength(8)]
        [Required]
        public string Password { get; set; }

        [Column("email", Order = 130)]
        [MaxLength(1000)]
        [MinLength(5)]
        [Required]
        public string Email { get; set; }

        [Column("name", Order = 140)]
        [MaxLength(300)]
        [Required]
        public string Name { get; set; }

        [Column("date_created", Order = 300)]
        [Required]
        public DateTime DateCreated { get; set; }

        [Column("date_modified", Order = 310)]
        public DateTime? DateModified { get; set; }

        public override string ToString()
        {
            return string.Format("AccountUser({0}): username={1}", this.ID, this.Username);
        }
    }
}
