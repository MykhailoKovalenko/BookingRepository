using AutoMapper;
using BookingRooms.ActionFilters;
using BookingRooms.Controllers;
using BookingRooms.Interfaces;
using BookingRooms.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookingRooms.Tests
{
    public class RoomControllerTests
    {
        [Fact]
        public async Task GetRoom_UnexistingItem_NotFound()
        {
            // Arrange
            var serviceStub = new Mock<IRoomService>();
            serviceStub.Setup(s => s.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((Room)null);

            var mapperStub = new Mock<IMapper>();

            var controller = new RoomController(serviceStub.Object, mapperStub.Object);

            // Act
            var result = await controller.GetRoom(0);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            typeof(RoomController).GetMethod(nameof(RoomController.GetRoom)).Should().BeDecoratedWith<AsyncActionFilterRoomIdValidation>();
        }
    }
}
