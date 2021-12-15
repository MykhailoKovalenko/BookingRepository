using AutoMapper;
using BookingRooms.ActionFilters;
using BookingRooms.Controllers;
using BookingRooms.Interfaces;
using BookingRooms.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SharedBookingLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookingRooms.Tests
{
    public class RoomControllerTests
    {
        private readonly Mock<IRoomService> serviceStub = new Mock<IRoomService>();
        private readonly Mock<IMapper> mapperStub = new Mock<IMapper>();
        private readonly Random rand = new Random();

        [Fact]
        public async Task GetRoom_UnexistingItem_NotFound()
        {
            // Arrange
            serviceStub.Setup(s => s.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((Room)null);

            var controller = new RoomController(serviceStub.Object, mapperStub.Object);

            // Act
            var result = await controller.GetRoom(0);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
           // typeof(RoomController).GetMethod(nameof(RoomController.GetRoom)).Should().BeDecoratedWith<AsyncActionFilterRoomIdValidation>();
        }

        [Fact]
        public async Task GetRoom_ExistingItem_ExpectedRoom()
        {
            // Arrange
            var expectedRoom = CreateRandomRoom();

            serviceStub.Setup(s => s.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedRoom);

            var controller = new RoomController(serviceStub.Object, mapperStub.Object);

            // Act
            var result = await controller.GetRoom(expectedRoom.Id);

            // Assert
            result.Value.Should().BeEquivalentTo(
                expectedRoom,
                options => options.ComparingByMembers<Room>()
                );
        }

        private Room CreateRandomRoom()
        {
            return new Room
            {
                Id = rand.Next(1, 100),
                Name = "test room",
                Places = rand.Next(1, 100),
                IsProjector = true
            };
        }
    }
}
