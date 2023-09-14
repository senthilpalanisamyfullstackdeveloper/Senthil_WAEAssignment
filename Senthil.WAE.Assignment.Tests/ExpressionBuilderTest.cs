namespace Senthil.WAE.Assignment.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using Senthil.WAE.Assignment.Model;
    using Senthil.WAE.Assignment.Utilities;

    [TestFixture]
    [Category("ExpressionBuilderTests")]
    public class ExpressionBuilderTest
    {
        [Test]
        public void GetExpressionUpperCaseChannelTest()
        {
            List<Filter> filter = new List<Filter>()
                            {
                                new Filter { PropertyName = "Channel", Operation = Helper.Operators.Equal, Value = "CHANNEL 1" },
                                new Filter { PropertyName = "Value", Operation = Helper.Operators.Equal, Value = Convert.ToDecimal("2.0")  },
                            };

            var query = ExpressionBuilder.GetExpression<Formula>(filter).Compile();

            // Act
            var result = FormulaTestData.Where(query).ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetExpressionEqualTest()
        {
            List<Filter> filter = new List<Filter>()
                            {
                                new Filter { PropertyName = "Channel", Operation = Helper.Operators.Equal, Value = "channel 1" },
                                new Filter { PropertyName = "Value", Operation = Helper.Operators.Equal, Value = Convert.ToDecimal("2.0")  },
                            };

            var query = ExpressionBuilder.GetExpression<Formula>(filter).Compile();

            // Act
            var result = FormulaTestData.Where(query).ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetExpressionGreaterThanTest()
        {
            List<Filter> filter = new List<Filter>()
                            {
                                new Filter { PropertyName = "Channel", Operation = Helper.Operators.Equal, Value = "channel 2" },
                                new Filter { PropertyName = "Value", Operation = Helper.Operators.GreaterThan, Value = Convert.ToDecimal("2.0")  },
                            };

            var query = ExpressionBuilder.GetExpression<Formula>(filter).Compile();

            // Act
            var result = FormulaTestData.Where(query).ToList();

            // Assert
            //Excluding value 2 the total count would be 5
            Assert.That(result.Count, Is.EqualTo(5));
        }

        [Test]
        public void GetExpressionGreaterThanOrEqualsToTest()
        {
            List<Filter> filter = new List<Filter>()
                            {
                                new Filter { PropertyName = "Channel", Operation = Helper.Operators.Equal, Value = "channel 2" },
                                new Filter { PropertyName = "Value", Operation = Helper.Operators.GreaterThanOrEqualsTo, Value = Convert.ToDecimal("2.0")  },
                            };

            var query = ExpressionBuilder.GetExpression<Formula>(filter).Compile();

            // Act
            var result = FormulaTestData.Where(query).ToList();

            // Assert
            //Including value 2 the total count would be 6
            Assert.That(result.Count, Is.EqualTo(6));
        }

        [Test]
        public void GetExpressionLessThanTest()
        {
            List<Filter> filter = new List<Filter>()
                            {
                                new Filter { PropertyName = "Channel", Operation = Helper.Operators.Equal, Value = "channel 3" },
                                new Filter { PropertyName = "Value", Operation = Helper.Operators.LessThan, Value = Convert.ToDecimal("3")  },
                            };

            var query = ExpressionBuilder.GetExpression<Formula>(filter).Compile();

            // Act
            var result = FormulaTestData.Where(query).ToList();

            // Assert
            //Excluding the value 3 for channel 3 the total count would be 14
            Assert.That(result.Count, Is.EqualTo(13));
        }

        [Test]
        public void GetExpressionLessThanOrEqualsToTest()
        {
            List<Filter> filter = new List<Filter>()
                            {
                                new Filter { PropertyName = "Channel", Operation = Helper.Operators.Equal, Value = "channel 3" },
                                new Filter { PropertyName = "Value", Operation = Helper.Operators.LessThanOrEqualsTo, Value = Convert.ToDecimal("3")  },
                            };

            var query = ExpressionBuilder.GetExpression<Formula>(filter).Compile();

            // Act
            var result = FormulaTestData.Where(query).ToList();

            // Assert
            //Including the value 3 for channel 3 the total count would be 14
            Assert.That(result.Count, Is.EqualTo(14));
        }

        [Test]
        public void GetExpressionNotEqualTest()
        {
            List<Filter> filter = new List<Filter>()
                            {
                                new Filter { PropertyName = "Channel", Operation = Helper.Operators.Equal, Value = "channel 3" },
                                new Filter { PropertyName = "Value", Operation = Helper.Operators.NotEqual, Value = Convert.ToDecimal("4")  },
                            };

            var query = ExpressionBuilder.GetExpression<Formula>(filter).Compile();

            // Act
            var result = FormulaTestData.Where(query).ToList();

            // Assert
            //ExIncluding the value 4 for channel 3 and remaing all the value total count would be 18
            Assert.That(result.Count, Is.EqualTo(18));
        }

        private readonly static Formula[] FormulaTestData =
        {
            new Formula() { TimeSpan = new TimeSpan(1,1,1), Value = Convert.ToDecimal("-0.1"), Channel = "channel 1" },
            new Formula() { TimeSpan = new TimeSpan(2,1,1), Value = Convert.ToDecimal("0.0"), Channel = "channel 1" },
            new Formula() { TimeSpan = new TimeSpan(3,1,1), Value = Convert.ToDecimal("1"), Channel = "Channel 1" },
            new Formula() { TimeSpan = new TimeSpan(4,1,1), Value = Convert.ToDecimal("2"), Channel = "channel 1" },
            new Formula() { TimeSpan = new TimeSpan(5,1,1), Value = Convert.ToDecimal("1.1"), Channel = "channel 1" },
            new Formula() { TimeSpan = new TimeSpan(6,1,1), Value = Convert.ToDecimal("2.1"), Channel = "channel 1" },
            new Formula() { TimeSpan = new TimeSpan(7,1,1), Value = Convert.ToDecimal("3.12"), Channel = "channel 1" },
            new Formula() { TimeSpan = new TimeSpan(8,1,1), Value = Convert.ToDecimal("4.0"), Channel = "channel 1" },
            new Formula() { TimeSpan = new TimeSpan(9,1,1), Value = Convert.ToDecimal("5"), Channel = "channel 1" },
            new Formula() { TimeSpan = new TimeSpan(10,1,1), Value = Convert.ToDecimal("100.00"), Channel = "channel 1" },
            new Formula() { TimeSpan = new TimeSpan(11,1,1), Value = Convert.ToDecimal("1"), Channel = "Channel 2" },
            new Formula() { TimeSpan = new TimeSpan(12,1,1), Value = Convert.ToDecimal("0.02"), Channel = "channel 2" },
            new Formula() { TimeSpan = new TimeSpan(1,1,1), Value = Convert.ToDecimal("-0.12"), Channel = "channel 2" },
            new Formula() { TimeSpan = new TimeSpan(1,1,1), Value = Convert.ToDecimal("-0.1"), Channel = "channel 2" },
            new Formula() { TimeSpan = new TimeSpan(2,1,1), Value = Convert.ToDecimal("0.0"), Channel = "channel 2" },
            new Formula() { TimeSpan = new TimeSpan(3,1,1), Value = Convert.ToDecimal("1"), Channel = "channel 2" },
            new Formula() { TimeSpan = new TimeSpan(4,1,1), Value = Convert.ToDecimal("2"), Channel = "channel 2" },
            new Formula() { TimeSpan = new TimeSpan(5,1,1), Value = Convert.ToDecimal("1.1"), Channel = "channel 2" },
            new Formula() { TimeSpan = new TimeSpan(6,1,1), Value = Convert.ToDecimal("2.1"), Channel = "channel 2" },
            new Formula() { TimeSpan = new TimeSpan(7,1,1), Value = Convert.ToDecimal("3.12"), Channel = "channel 2" },
            new Formula() { TimeSpan = new TimeSpan(8,1,1), Value = Convert.ToDecimal("4.0"), Channel = "channel 2" },
            new Formula() { TimeSpan = new TimeSpan(9,1,1), Value = Convert.ToDecimal("5"), Channel = "channel 2" },
            new Formula() { TimeSpan = new TimeSpan(10,1,1), Value = Convert.ToDecimal("100.00"), Channel = "channel 2" },
            new Formula() { TimeSpan = new TimeSpan(11,1,1), Value = Convert.ToDecimal("1"), Channel = "Channel 3" },
            new Formula() { TimeSpan = new TimeSpan(12,1,1), Value = Convert.ToDecimal("0.02"), Channel = "channel 3" },
            new Formula() { TimeSpan = new TimeSpan(1,1,1), Value = Convert.ToDecimal("-0.12"), Channel = "channel 3" },
            new Formula() { TimeSpan = new TimeSpan(11,1,1), Value = Convert.ToDecimal("1"), Channel = "channel 3" },
            new Formula() { TimeSpan = new TimeSpan(12,1,1), Value = Convert.ToDecimal("0.02"), Channel = "channel 3" },
            new Formula() { TimeSpan = new TimeSpan(1,1,1), Value = Convert.ToDecimal("-0.12"), Channel = "channel 3" },
            new Formula() { TimeSpan = new TimeSpan(1,1,1), Value = Convert.ToDecimal("-0.1"), Channel = "channel 3" },
            new Formula() { TimeSpan = new TimeSpan(2,1,1), Value = Convert.ToDecimal("0.0"), Channel = "channel 3" },
            new Formula() { TimeSpan = new TimeSpan(3,1,1), Value = Convert.ToDecimal("1"), Channel = "channel 3" },
            new Formula() { TimeSpan = new TimeSpan(4,1,1), Value = Convert.ToDecimal("2"), Channel = "channel 3" },
            new Formula() { TimeSpan = new TimeSpan(5,1,1), Value = Convert.ToDecimal("1.1"), Channel = "channel 3" },
            new Formula() { TimeSpan = new TimeSpan(6,1,1), Value = Convert.ToDecimal("2.1"), Channel = "channel 3" },
            new Formula() { TimeSpan = new TimeSpan(7,1,1), Value = Convert.ToDecimal("3.12"), Channel = "channel 3" },
            new Formula() { TimeSpan = new TimeSpan(8,1,1), Value = Convert.ToDecimal("3"), Channel = "channel 3" },
            new Formula() { TimeSpan = new TimeSpan(8,1,1), Value = Convert.ToDecimal("4.0"), Channel = "channel 3" },
            new Formula() { TimeSpan = new TimeSpan(9,1,1), Value = Convert.ToDecimal("5"), Channel = "channel 3" },
            new Formula() { TimeSpan = new TimeSpan(10,1,1), Value = Convert.ToDecimal("10.00"), Channel = "channel 3" },
            new Formula() { TimeSpan = new TimeSpan(10,10,1), Value = Convert.ToDecimal("50.00"), Channel = "channel 3" },
            new Formula() { TimeSpan = new TimeSpan(11,1,1), Value = Convert.ToDecimal("4"), Channel = "channel 3" },
            new Formula() { TimeSpan = new TimeSpan(11,3,1), Value = Convert.ToDecimal("4"), Channel = "channel 3" },
            new Formula() { TimeSpan = new TimeSpan(11,4,1), Value = Convert.ToDecimal("4"), Channel = "channel 3" },
            new Formula() { TimeSpan = new TimeSpan(11,6,1), Value = Convert.ToDecimal("4"), Channel = "channel 3" },
            new Formula() { TimeSpan = new TimeSpan(12,1,1), Value = Convert.ToDecimal("0.02"), Channel = "channel 3" },
            new Formula() { TimeSpan = new TimeSpan(1,1,1), Value = Convert.ToDecimal("-0.12"), Channel = "channel 3" }
        };
    }
}
