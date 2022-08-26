

namespace PhoneDirectoryLibrary.Models
{
    public class TitleModel
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public int TitleLevel {
            get => GetTitleLevel();
        }

        private int GetTitleLevel()
        {
            var titleName = Regex.Replace(Name, @"\s+", "");
            TitleLevelEnum titleLevel = (TitleLevelEnum)Enum.Parse(typeof(TitleLevelEnum), titleName);
            int result = (int)titleLevel;
            return result;

        }
    }

    enum TitleLevelEnum
    {
        VP = 1,
        Director = 2,
        AssistantDirector = 3,
        Manager = 4,
        AssistantManager = 5,
        Supervisor = 6,
        Untitled = 7
    }
}
