namespace Services.Test;

public class UserServiceTest
{
    private IUnitOfWork unitOfWork = null!;
    private UsersService usersService = null!;
    [SetUp]
    public void Setup()
    {
        unitOfWork = A.Fake<IUnitOfWork>();
        usersService = new UsersService(unitOfWork);
    }
    [Test]
    public async Task GetByIdAsync_ReturnsUser()
    {
        var id = A.Dummy<int>();
        var result = await usersService.GetByIdAsync(id);
        Assert.That(result, Is.InstanceOf<User>());
    }
    [Test]
    public async Task GetByNameAsync_ReturnsUser()
    {
        var name = A.Dummy<string>();
        var result = await usersService.GetByNameIncludingRolesAsync(name);
        Assert.That(result, Is.InstanceOf<User>());
    }
    [Test]
    public void AddUserToRoleAsync_RolesAreSame_ThrowsSameValueAssignException()
    {
        var roleName = "User";
        var user = DataGenerator.CreateTestUserWithRoleName(roleName);
        Assert.ThrowsAsync<SameValueAssignException>(async () =>
        {
            await usersService.AddUserToRoleAsync(user, roleName);
        });
    }
    [Test]
    public async Task AddUserToRoleAsync_RolesAreDifferent_CallsAssignToRoleAndCompleteAsync()
    {
        var oldRoleName = "Admin";
        var newRoleName = "User";
        var user = DataGenerator.CreateTestUserWithRoleName(oldRoleName);
        await usersService.AddUserToRoleAsync(user, newRoleName);
        A.CallTo(() => unitOfWork.CompleteOrThrowAsync()).MustHaveHappenedOnceExactly();
    }
    [Test]
    public async Task RemoveAsync_CallsRemove()
    {
        await usersService.RemoveAsync(A.Dummy<User>());
    }
}