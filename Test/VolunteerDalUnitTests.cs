using Data;
using Data.Constants;
using Data.Entities;
using DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Fixtures;

namespace Test
{
    public class VolunteerDalUnitTests : IClassFixture<VolunteerDalContextFixture>
    {
        VolunteerDalContextFixture contextFixture;
        public VolunteerDalUnitTests(VolunteerDalContextFixture contextFixture)
        {
            this.contextFixture = contextFixture;
        }

        [Fact]
        public void GetByKey_WhenCalledByKey_ThenReturnsUser()
        {
            // Assign
            var userDal = new VolunteerDal(contextFixture.context);

            /// Act
            var result = userDal.GetByKey(contextFixture.userKey).Result;

            /// Assert
            Assert.Equal(2, result.Id);
            Assert.Equal("volunteer2@gmail.com", result.Email);
        }
        [Fact]
        public void Cancel_WhenCancelled_ThenResultsTrueWithCancellationReason()
        {
            // Assign
            var userDal = new VolunteerDal(contextFixture.context);

            /// Act
            var volunteer = userDal.GetByIdAsync(1).Result;
            var result = userDal.Cancel(volunteer, "MyReason").Result;

            /// Assert
            Assert.False(result.Error);
            Assert.Equal(VolunteerStatus.Cancelled, volunteer.Status);
            Assert.Equal("MyReason", volunteer.CancellationReason);
        }
        [Fact]
        public void GetForDropDown_WhenCalled_ThenReturnsOnlyCompletedStatusVolunteers()
        {
            // Assign
            var userDal = new VolunteerDal(contextFixture.context);

            /// Act
            var result = userDal.GetForDropDown().Result;

            /// Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("3", result[0].Id);
        }

    }
}
