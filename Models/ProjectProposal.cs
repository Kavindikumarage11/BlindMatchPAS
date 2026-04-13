using System.ComponentModel.DataAnnotations;

namespace BlindMatchPAS.Models
{
    public class ProjectProposal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Abstract { get; set; } = string.Empty;

        [Required]
        public string TechStack { get; set; } = string.Empty;

        // Thama match unada nadda kiyala balanna
        public bool IsMatched { get; set; } = false;

        // Lead ge role ekata adala "Identity Reveal" logic ekata meka ona wenawa
        public bool IsIdentityRevealed { get; set; } = false;
    }
}