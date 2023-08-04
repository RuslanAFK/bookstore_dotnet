namespace Services.Test;

public class UserServiceTest
{
    private IUsersRepository usersRepository;
    private IRolesRepository rolesRepository;
    private IUnitOfWork unitOfWork;
    private UsersService usersService;
    [SetUp]
    public void Setup()
    {
        usersRepository = A.Fake<IUsersRepository>();
        rolesRepository = A.Fake<IRolesRepository>();
        unitOfWork = A.Fake<IUnitOfWork>();
        usersService = new UsersService(usersRepository, unitOfWork, rolesRepository);
    }
    [Test]
    public async Task GetQueriedAsync_ReturnsListResponseOfUser()
    {
        var query = A.Dummy<Query>();
        var result = await usersService.GetQueriedAsync(query);
        Assert.That(result, Is.InstanceOf<ListResponse<User>>());
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
        var result = await usersService.GetByNameAsync(name);
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
        A.CallTo(() => rolesRepository.AssignToRoleAsync(A<User>._, A<string>._)).MustHaveHappenedOnceExactly();
    }
    [Test]
    public async Task RemoveAsync_CallsRemove()
    {
        await usersService.RemoveAsync(A.Dummy<User>());
        A.CallTo(() => usersRepository.Remove(A<User>._)).MustHaveHappenedOnceExactly();
    }
}