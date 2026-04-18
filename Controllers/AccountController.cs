[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> ConfirmMatch(int proposalId)
{
    // Find the specific proposal from the database
    var proposal = await _context.ProjectProposals.FindAsync(proposalId);

    if (proposal != null)
    {
        // Logic: Once supervisor confirms, the 'Blind' state is removed
        proposal.IsMatched = true; 
        proposal.IsIdentityRevealed = true; // Identity is now visible to both parties

        _context.Update(proposal);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Match Confirmed! Student and Supervisor identities are now revealed.";
    }

    // Redirect back to the Supervisor Dashboard
    return RedirectToAction("Index", "Supervisor");
}
