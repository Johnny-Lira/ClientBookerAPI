using Application.DTOs;
using Application.Services;
using Bogus;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using ClientBooker.API.Domain.Exceptions;

namespace TestClientBooker.API
{
    [TestFixture]
    internal class ClientServiceTests
    {
        private Mock<IClientRepository> _mockRepo;
        private ClientService _service;
        private Faker _faker;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IClientRepository>();
            _service = new ClientService(_mockRepo.Object);
            _faker = new Faker();
        }

        [Test]
        public async Task Should_Create_Client_Successfully()
        {
            //Arrange
            var name = _faker.Name.FullName();
            var phone = _faker.Phone.PhoneNumber("###########");
            var email = _faker.Internet.Email(name.Replace(" ",""));

            var dto = new ClientDto
            {
                Name = name,
                Phone = phone,
                Email = email
            };

            _mockRepo.Setup(r => r.AddAsync(It.IsAny<Client>()))
                .ReturnsAsync((Client c) => c);

            //Act
            var result = await _service.CreateAsync(dto);

            //Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(dto.Name);
            result.Phone.Should().Be(dto.Phone);
            result.Email.Should().Be(dto.Email.ToLower());
        }

        [Test]
        public async Task Should_Throw_Exception_When_Client_Already_Exists()
        {
            //Arrange
            var name = _faker.Name.FullName();
            var phone = _faker.Phone.PhoneNumber("###########");
            var email = _faker.Internet.Email(name.Replace(" ", ""));

            var dto = new ClientDto
            {
                Name = name,
                Phone = phone,
                Email = email
            };

            var existingClient = new Client(dto.Name, dto.Phone, dto.Email);

            _mockRepo.Setup(r => r.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(existingClient);

            //Act
            Func<Task> result = async () => await _service.CreateAsync(dto);

            //Assert
            result.Should().NotBeNull();
            await result.Should().ThrowAsync<ClientAlreadyExistsException>()
                .WithMessage($"Client with email {dto.Email} already exists.");

        }
    }
}
