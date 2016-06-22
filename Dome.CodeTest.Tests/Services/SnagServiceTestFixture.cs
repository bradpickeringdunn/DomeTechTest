using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dome.CodeTest.DataAccess;
using Dome.CodeTest.Services;
using FakeItEasy;
using System.Collections.Generic;
using Dome.CodeTest.Models;

namespace Dome.CodeTest.Tests
{
    [TestClass]
    public class SnagServiceTestFixture
    {
        private IFailoverSnagDataAccess failoverSnagDataAccess;
        private ISnagDataAccess dataAccess;
        private IArchivedDataService archivedDataService;
        private IFailoverRepository failoverRepository;

        [TestMethod]
        public void Ensure_If_IsSnagArchived_True_Snag_Is_Returned_From_ArchiveDataStore()
        {
            failoverSnagDataAccess = A.Fake<IFailoverSnagDataAccess>();
            dataAccess = A.Fake<ISnagDataAccess>();
            archivedDataService = A.Fake<IArchivedDataService>();
            failoverRepository = A.Fake<IFailoverRepository>();

            var snag = new Snag()
            {
                Id = 1,
                Name = "Test snag from archive",
            };

            A.CallTo(() => archivedDataService.GetArchivedSnag(0)).WithAnyArguments().Returns(snag);

            var snagService = new SnagService(failoverSnagDataAccess, dataAccess, archivedDataService, failoverRepository);
            var result = snagService.GetSnag(1, true);

            Assert.AreEqual(snag.Id, result.Id);
            Assert.AreEqual(snag.Name, result.Name);
            A.CallTo(() => dataAccess.LoadSnag(0)).WithAnyArguments().MustNotHaveHappened();
            A.CallTo(() => failoverRepository.GetFailOverEntries()).WithAnyArguments().MustNotHaveHappened();
            A.CallTo(() => failoverSnagDataAccess.GetSnagById(0)).WithAnyArguments().MustNotHaveHappened();
        }

        [TestMethod]
        public void Ensure_If_IsSnagArchived_False_And_NotInFailoverMode_Load_Snag_From_DataAccessStore()
        {
            failoverSnagDataAccess = A.Fake<IFailoverSnagDataAccess>();
            dataAccess = A.Fake<ISnagDataAccess>();
            archivedDataService = A.Fake<IArchivedDataService>();
            failoverRepository = A.Fake<IFailoverRepository>();

            var failoverEnties = new List<FailoverEntry>()
            {
                new FailoverEntry() { DateTime = DateTime.Now.AddMinutes(20)},
            };

            A.CallTo(() => failoverRepository.GetFailOverEntries()).WithAnyArguments().Returns(failoverEnties);

            var snag = new Snag()
            {
                Id = 1,
                Name = "Test snag from datastore",
            };

            A.CallTo(() => dataAccess.LoadSnag(0)).WithAnyArguments().Returns(new SnagResponse()
            {
                IsArchived = false,
                Snag = snag
            });

            var snagService = new SnagService(failoverSnagDataAccess, dataAccess, archivedDataService, failoverRepository);
            var result = snagService.GetSnag(1, false);

            Assert.AreEqual(snag.Id, result.Id);
            Assert.AreEqual(snag.Name, result.Name);

            A.CallTo(() => archivedDataService.GetArchivedSnag(0)).WithAnyArguments().MustNotHaveHappened();
        }

        [TestMethod]
        public void Ensure_If_IsSnagArchived_False_And_InFailoverMode_Load_Snag_From_FailoverStore()
        {
            failoverSnagDataAccess = A.Fake<IFailoverSnagDataAccess>();
            dataAccess = A.Fake<ISnagDataAccess>();
            archivedDataService = A.Fake<IArchivedDataService>();
            failoverRepository = A.Fake<IFailoverRepository>();

            var failoverEnties = new List<FailoverEntry>();

            for (var entity = 1; entity < 110; entity++)
            {
                failoverEnties.Add(new FailoverEntry() { DateTime = DateTime.Now });
            };

            A.CallTo(() => failoverRepository.GetFailOverEntries()).WithAnyArguments().Returns(failoverEnties);

            var snag = new Snag()
            {
                Id = 1,
                Name = "Test snag from failover snag data access store",
            };

            A.CallTo(() => failoverSnagDataAccess.GetSnagById(1)).WithAnyArguments().Returns(new SnagResponse()
            {
                IsArchived = false,
                Snag = snag
            });

            var snagService = new SnagService(failoverSnagDataAccess, dataAccess, archivedDataService, failoverRepository);
            var result = snagService.GetSnag(1, false);

            Assert.AreEqual(snag.Id, result.Id);
            Assert.AreEqual(snag.Name, result.Name);

            A.CallTo(() => dataAccess.LoadSnag(0)).WithAnyArguments().MustNotHaveHappened();
            A.CallTo(() => archivedDataService.GetArchivedSnag(0)).WithAnyArguments().MustNotHaveHappened();
        }


    }
}
