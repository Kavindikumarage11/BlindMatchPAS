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

        // Added Category property to resolve the controller error
        [Required]
        public string Category { get; set; } = string.Empty;

        // Tracks if the proposal has been matched/approved by a supervisor
        public bool IsMatched { get; set; } = false;

        // Logic for revealing the student's identity after a match is made
        public bool IsIdentityRevealed { get; set; } = false;
    }
}