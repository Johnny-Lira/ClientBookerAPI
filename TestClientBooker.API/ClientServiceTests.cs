using System;
using Application.DTOs;
using Application.Services;
using Bogus;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

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
            var dto = new ClientDto
            {
                Name = _faker.Name.FullName(),
                Phone = _faker.Phone.PhoneNumber("###########"),
                Email = _faker.Internet.Email()
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
    }
}
