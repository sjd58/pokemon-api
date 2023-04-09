using Microsoft.VisualStudio.TestTools.UnitTesting;
using pokemon_api;

namespace pokemon_api_tests
{
    [TestClass]
    public class PokemonSqlDaoTests
    {
        private string[] TestStringArray = new string[]
        {
            "1",
            "TestMon",
            "Normal",
            "",
            "600",
            "100",
            "100",
            "100",
            "100",
            "100",
            "100",
            "1",
            "False"
        };

        private static readonly Pokemon ExpectedPokemon = new Pokemon
        {
            PokemonNumber = 1,
            Name = "TestMon",
            Type1 = "Normal",
            Type2 = "",
            Total = 600,
            HP = 100,
            Attack = 100,
            Defense = 100,
            SpAttack = 100,
            SpDefense = 100,
            Speed = 100,
            Generation = 1,
            Legendary = false
        };

        [TestMethod]
        public void SteelType1HPDoubled()
        {
            // Arrange
            ExpectedPokemon.HP = 200;
            ExpectedPokemon.Total = 700;
            TestStringArray[3] = "Psychic";
            TestStringArray[2] = "Steel";

            // Act
            Pokemon actualMon = new Pokemon(TestStringArray);

            // Assert
            Assert.AreEqual(ExpectedPokemon.HP, actualMon.HP);
            Assert.AreEqual(ExpectedPokemon.Total, actualMon.Total);
        }

        [TestMethod]
        public void SteelType2HPDouble()
        {
            // Arrange
            ExpectedPokemon.HP = 200;
            ExpectedPokemon.Total = 700;
            TestStringArray[3] = "Steel";
            TestStringArray[2] = "Water";

            // Act
            Pokemon actualMon = new Pokemon(TestStringArray);

            // Assert
            Assert.AreEqual(ExpectedPokemon.HP, actualMon.HP);
            Assert.AreEqual(ExpectedPokemon.Total, actualMon.Total);
        }

        [TestMethod]
        public void FireType1AttackLowered()
        {
            // Arrange
            ExpectedPokemon.Attack = 90;
            ExpectedPokemon.Total = 590;
            TestStringArray[2] = "Fire";
            TestStringArray[3] = "Dark";

            // Act
            Pokemon actualMon = new Pokemon(TestStringArray);

            // Assert
            Assert.AreEqual(ExpectedPokemon.Attack, actualMon.Attack);
            Assert.AreEqual(ExpectedPokemon.Total, actualMon.Total);
        }

        [TestMethod]
        public void FireType2AttackLowered()
        {
            // Arrange
            ExpectedPokemon.Attack = 90;
            ExpectedPokemon.Total = 590;
            TestStringArray[2] = "Ghost";
            TestStringArray[3] = "Fire";

            // Act
            Pokemon actualMon = new Pokemon(TestStringArray);

            // Assert
            Assert.AreEqual(ExpectedPokemon.Attack, actualMon.Attack);
            Assert.AreEqual(ExpectedPokemon.Total, actualMon.Total);
        }

        [TestMethod]
        public void BugFlyingSpeedIncreased()
        {
            // Arrange
            ExpectedPokemon.Speed = 110;
            ExpectedPokemon.Total = 610;
            TestStringArray[2] = "Bug";
            TestStringArray[3] = "Flying";

            // Act
            Pokemon actualMon = new Pokemon(TestStringArray);

            // Assert
            Assert.AreEqual(ExpectedPokemon.Speed, actualMon.Speed);
            Assert.AreEqual(ExpectedPokemon.Total, actualMon.Total);
        }

        [TestMethod]
        public void FlyingBugSpeedIncreased()
        {
            // Arrange
            ExpectedPokemon.Speed = 110;
            ExpectedPokemon.Total = 610;
            TestStringArray[2] = "Flying";
            TestStringArray[3] = "Bug";

            // Act
            Pokemon actualMon = new Pokemon(TestStringArray);

            // Assert
            Assert.AreEqual(ExpectedPokemon.Speed, actualMon.Speed);
            Assert.AreEqual(ExpectedPokemon.Total, actualMon.Total);
        }

        [TestMethod]
        public void GNameDefenseIncreased()
        {
            // Arrange
            TestStringArray[1] = "GNameTest";
            ExpectedPokemon.Defense = 140;
            ExpectedPokemon.Total = 640;

            // Act
            Pokemon actualMon = new Pokemon(TestStringArray);

            // Assert
            Assert.AreEqual(ExpectedPokemon.Defense, actualMon.Defense);
            Assert.AreEqual(ExpectedPokemon.Total, actualMon.Total);
        }

        [TestMethod]
        public void GNameNoOtherChars()
        {
            // Arrange
            TestStringArray[1] = "G";
            ExpectedPokemon.Defense = 100;
            ExpectedPokemon.Total = 600;

            // Act
            Pokemon actualMon = new Pokemon(TestStringArray);

            // Assert
            Assert.AreEqual(ExpectedPokemon.Defense, actualMon.Defense);
            Assert.AreEqual(ExpectedPokemon.Total, actualMon.Total);
        }
    }
}
