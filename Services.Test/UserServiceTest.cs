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
    public async Task AddUserToRoleAsync_RolesAreDifferent_CallsAssignToRoleAndCompleteAsync()
    {
        var oldRoleName = "Admin";
        var newRoleName = "User";
        var user = DataGenerator.CreateTestUserWithRoleName(oldRoleName);
        await usersService.AddUserToRoleAsync(A.Dummy<int>(), newRoleName);
        A.CallTo(() => unitOfWork.CompleteAsync()).MustHaveHappenedOnceExactly();
    }
    [Test]
    public async Task RemoveAsync_CallsRemove()
    {
        await usersService.RemoveAsync(A.Dummy<int>());
    }
}