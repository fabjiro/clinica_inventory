namespace Domain.Repository;

public interface IUploaderRepository {
    Task<string> Upload(string base64);

    Task<string> Delete(string url);
}