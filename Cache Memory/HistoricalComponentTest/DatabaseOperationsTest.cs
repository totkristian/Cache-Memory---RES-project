﻿using HistoricalComponent;
using ModelsAndProps.Historical;
using ModelsAndProps.ValueStructure;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoricalComponentTest
{
    [TestFixture]
    public class DatabaseOperationsTest
    {
        
        Mock<DatabaseOperations> dataMock;
        Mock<HistoricalProperty> hpMock;
        Mock<HistoricalProperty> hpTempMock;
        Mock<Value> valueMock;
        Mock<Value> valueMock1;

        [SetUp]
        public void SetUp()
        {
            dataMock = new Mock<DatabaseOperations>();
            hpMock = new Mock<HistoricalProperty>();
            hpTempMock = new Mock<HistoricalProperty>();
            valueMock = new Mock<Value>();
            valueMock.Object.Consumption = 10.0;
            valueMock.Object.GeographicalLocationId = Guid.NewGuid().ToString();
            valueMock.Object.Timestamp = DateTime.Now;
           
            valueMock1 = new Mock<Value>();
            valueMock1.Object.Consumption = 15.0;
            valueMock1.Object.GeographicalLocationId = Guid.NewGuid().ToString();
            valueMock1.Object.Timestamp = DateTime.Now;

        }
        #region checkGeoId
        [Test]
        [TestCase("ASDASDAS")]
        [TestCase("AAAA")]
        [TestCase("BBB")]
        public void CheckGeoIdGoodParameters(string id)
        {
            Assert.DoesNotThrow(() =>
            {
                dataMock.Object.CheckGeoId(id);
            });
        }
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("       ")]
        public void CheckGeoIdBadParameters(string id)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                dataMock.Object.CheckGeoId(id);
            });
        }
        #endregion

        #region checkDeadBand
        [Test]
        public void CheckDeadBandGoodParameters()
        {
            hpMock.Object.Code = Codes.CODE_DIGITAL;
            hpTempMock.Object.Code = Codes.CODE_DIGITAL;
            Assert.DoesNotThrow(() =>
            {
                dataMock.Object.CheckDeadband(hpMock.Object, hpTempMock.Object);
            });
            Assert.IsTrue(dataMock.Object.CheckDeadband(hpMock.Object, hpTempMock.Object));
        }
        [Test]
        public void CheckDeadBandGoodParameters1()
        {
            hpMock.Object.Code = Codes.CODE_MOTION;
            hpMock.Object.HistoricalValue = valueMock.Object;
            hpTempMock.Object.Code = Codes.CODE_MOTION;
            hpTempMock.Object.HistoricalValue = valueMock1.Object;
            
            Assert.DoesNotThrow(() =>
            {
                dataMock.Object.CheckDeadband(hpMock.Object, hpTempMock.Object);
            });
            Assert.IsTrue(dataMock.Object.CheckDeadband(hpMock.Object, hpTempMock.Object));
        }

        [Test]
        public void CheckDeadBandGoodParameters2()
        {
            hpMock.Object.Code = Codes.CODE_MOTION;
            hpMock.Object.HistoricalValue = valueMock.Object;
            hpTempMock.Object.Code = Codes.CODE_MOTION;
            valueMock1.Object.Consumption = 10.0;
            hpTempMock.Object.HistoricalValue = valueMock1.Object;

            Assert.DoesNotThrow(() =>
            {
                dataMock.Object.CheckDeadband(hpMock.Object, hpTempMock.Object);
            });
            Assert.IsFalse(dataMock.Object.CheckDeadband(hpMock.Object, hpTempMock.Object));
        }

        [Test]
        public void CheckDeadBandBadParameters()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                dataMock.Object.CheckDeadband(null, hpTempMock.Object);
            });
           
        }
        [Test]
        public void CheckDeadBandBadParameters1()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                dataMock.Object.CheckDeadband(hpMock.Object, null);
            });

        }
        [Test]
        public void CheckDeadBandBadParameters2()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                dataMock.Object.CheckDeadband(null, null);
            });

        }

        [Test]
        public void CheckDeadBandBadParameters3()
        {
            hpMock.Object.Code = Codes.CODE_SINGLENODE;
            Assert.Throws<ArgumentException>(() =>
            {
                dataMock.Object.CheckDeadband(hpMock.Object, hpTempMock.Object);
            });

        }
        #endregion
    }
}
