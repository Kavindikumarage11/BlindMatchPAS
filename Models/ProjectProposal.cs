using System.ComponentModel.DataAnnotations;

namespace BlindMatchPAS.Models
{
    public class ProjectProposal
    {
        // Primary Key for the Database
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Abstract { get; set; } = string.Empty;

        [Required]
        public string TechStack { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        // Logic for Blind Matching
        public bool IsMatched { get; set; } = false;

        // Logic for Identity Reveal
        public bool IsIdentityRevealed { get; set; } = false;
    }
}