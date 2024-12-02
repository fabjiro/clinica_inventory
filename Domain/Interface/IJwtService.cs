namespace Domain.Interface;

public interface IJwtService
{
    string GenerateToken(string userId, int time = 8);
    
    bool ValidateToken(string token);

    string? GetUserIdFromToken(string token);
}