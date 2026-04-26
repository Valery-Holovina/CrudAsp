using Microsoft.EntityFrameworkCore;

public class UserService
{
    private readonly UsersContext _db;

    public UserService(UsersContext db)
    {
        _db = db;
    }

    // GET ALL
    public async Task<List<Users>> GetAll()
    {
        return await _db.Users.ToListAsync();
    }

    // CREATE
    public async Task<Users> Create(Users user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    // UPDATE
    public async Task<Users?> Update(int id, Users updated)
    {
        var user = await _db.Users.FindAsync(id);
        if (user == null) return null;

        user.Name = updated.Name;
        user.Age = updated.Age;
        user.Email = updated.Email;

        await _db.SaveChangesAsync();
        return user;
    }

    // DELETE
    public async Task<bool> Delete(int id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user == null) return false;

        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
        return true;
    }
}