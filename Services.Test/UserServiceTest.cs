namespace Services.Test
{
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
        public async Task GetQueriedAsync__ReturnsListResponseOfUser()
        {
            var result = await usersService.GetQueriedAsync(A.Dummy<Query>());
            Assert.That(result, Is.InstanceOf<ListResponse<User>>());
        }
        [Test]
        public async Task GetByIdAsync__ReturnsUser()
        {
            var result = await usersService.GetByIdAsync(0);
            Assert.That(result, Is.InstanceOf<User>());
        }
        [Test]
        public async Task GetByNameAsync__ReturnsUser()
        {
            var result = await usersService.GetByNameAsync(A.Dummy<string>());
            Assert.That(result, Is.InstanceOf<User>());
        }
        [Test]
        public void AddUserToRoleAsync_RolesAreSame_ThrowsSameValueAssignExceptionAndNotCallsAssignToRoleAndCompleteAsync()
        {
            var user = new User()
            {
                Role = new Role()
                {
                    RoleName = "User"
                }
            };
            var roleName = "User";
            Assert.ThrowsAsync<SameValueAssignException>(async () =>
            {
                await usersService.AddUserToRoleAsync(user, roleName);
            });
            A.CallTo(() => unitOfWork.CompleteOrThrowAsync()).MustNotHaveHappened();
            A.CallTo(() => rolesRepository.AssignToRoleAsync(A<User>._, A<string>._)).MustNotHaveHappened();
        }
        [Test]
        public void AddUserToRoleAsync_RolesAreDifferent_CallsAssignToRoleAndCompleteAsync()
        {
            var user = new User()
            {
                Role = new Role()
                {
                    RoleName = "User"
                }
            };
            var roleName = "Admin";
            Assert.DoesNotThrowAsync(async () =>
            {
                await usersService.AddUserToRoleAsync(user, roleName);
            });
            A.CallTo(() => unitOfWork.CompleteOrThrowAsync()).MustHaveHappenedOnceExactly();
            A.CallTo(() => rolesRepository.AssignToRoleAsync(A<User>._, A<string>._)).MustHaveHappenedOnceExactly();
        }
        [Test]
        public void RemoveAsync__CallsRemove()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                await usersService.RemoveAsync(A.Dummy<User>());
            });
            A.CallTo(() => usersRepository.Remove(A<User>._)).MustHaveHappenedOnceExactly();
        }
    }
}
