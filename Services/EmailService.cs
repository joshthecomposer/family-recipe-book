using MyApp.DataStorage;

namespace MyApp.Services;
public class EmailService :IEmailService
{
    private readonly DBContext _db;

    public EmailService(DBContext db)
    {
        _db = db;
    }
}
