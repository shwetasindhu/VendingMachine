namespace VendingMachine
{

    public static class VendingMachineTests
    {
        public static void RunTests()
        {
            Console.WriteLine("Running Vending Machine Tests...\n");

            TestCoinAcceptance();
            TestCoinRejection();
            TestProductPurchase();
            TestInsufficientFunds();
            TestDisplayBehavior();

            Console.WriteLine("All tests completed!\n");
        }

        private static void TestCoinAcceptance()
        {
            Console.WriteLine("Test: Coin Acceptance");
            var machine = new VendingMachine();

            machine.InsertCoin(CoinType.Quarter);
            Assert(machine.GetCurrentAmount() == 0.25m, "Quarter should be accepted");

            machine.InsertCoin(CoinType.Dime);
            Assert(machine.GetCurrentAmount() == 0.35m, "Dime should be accepted");

            machine.InsertCoin(CoinType.Nickel);
            Assert(machine.GetCurrentAmount() == 0.40m, "Nickel should be accepted");

            Console.WriteLine("✓ Coin acceptance test passed\n");
        }
        private static void TestCoinRejection()
        {
            Console.WriteLine("Test: Coin Rejection");
            var machine = new VendingMachine();

            machine.InsertCoin(CoinType.Penny);
            Assert(machine.GetCurrentAmount() == 0.00m, "Penny should be rejected");

            var coinReturn = machine.GetCoinReturn();
            Assert(coinReturn.Count == 1 && coinReturn[0] == CoinType.Penny, "Penny should be in coin return");

            Console.WriteLine("✓ Coin rejection test passed\n");
        }

        private static void TestProductPurchase()
        {
            Console.WriteLine("Test: Product Purchase");
            var machine = new VendingMachine();

            // Insert enough money for chips ($0.50)
            machine.InsertCoin(CoinType.Quarter);
            machine.InsertCoin(CoinType.Quarter);

            machine.SelectProduct(ProductType.Chips);
            Assert(machine.CheckDisplay() == "THANK YOU", "Should show THANK YOU after purchase");
            Assert(machine.CheckDisplay() == "INSERT COIN", "Should show INSERT COIN after checking display again");

            Console.WriteLine("✓ Product purchase test passed\n");
        }
        private static void TestInsufficientFunds()
        {
            Console.WriteLine("Test: Insufficient Funds");
            var machine = new VendingMachine();

            machine.InsertCoin(CoinType.Quarter); // $0.25
            machine.SelectProduct(ProductType.Cola); // $1.00

            Assert(machine.CheckDisplay() == "PRICE $1.00", "Should show price when insufficient funds");
            Assert(machine.CheckDisplay() == "$0.25", "Should show current amount after price display");

            Console.WriteLine("✓ Insufficient funds test passed\n");
        }

        private static void TestDisplayBehavior()
        {
            Console.WriteLine("Test: Display Behavior");
            var machine = new VendingMachine();

            Assert(machine.CheckDisplay() == "INSERT COIN", "Should show INSERT COIN initially");

            machine.InsertCoin(CoinType.Dime);
            Assert(machine.CheckDisplay() == "$0.10", "Should show current amount when money inserted");

            Console.WriteLine("✓ Display behavior test passed\n");
        }

        private static void Assert(bool condition, string message)
        {
            if (!condition)
            {
                throw new Exception($"Test failed: {message}");
            }
        }
    }
}
    

    
