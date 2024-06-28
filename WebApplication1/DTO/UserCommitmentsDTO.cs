namespace WebApplication1.DTO;

public class UserCommitmentsDTO
{
    public int idSubscription { get; set; }
    public ServiceDTO service { get; set; }
    public List<CommitmentDTO> commitments { get; set; }
}
