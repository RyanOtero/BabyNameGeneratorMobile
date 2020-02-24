using SQLite;

namespace NameGeneratorMobile.Model {
    [Table("girl_names")]

    public class GirlName {
        [PrimaryKey, Column("name")]
        public string Name { get; set; }

    }

    [Table("boy_names")]

    public class BoyName {
        [PrimaryKey, Column("name")]
        public string Name { get; set; }

    }
}
