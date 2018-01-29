using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainTest.Class;

namespace BlockchainTest
{
    class Program
    {
        //JAVA 코드로 이해하는 블록체인(BLOCKCHAIN) 
        //링크 : http://guruble.com/java-%EC%BD%94%EB%93%9C%EB%A1%9C-%EC%9D%B4%ED%95%B4%ED%95%98%EB%8A%94-%EB%B8%94%EB%A1%9D%EC%B2%B4%EC%9D%B8blockchain/#comment-214
        List<Block> blockchain = new List<Block>();

        static void Main(string[] args)
        {
            // Genesis block
            string[] transactions = { "DSKIM Sent 1k Bitcoins to ACS." };
            // 최초 노드 : genesisBlock
            BlockHeader blockheader = new BlockHeader(null, transactions);
            Block genesisBlock = new Block(blockheader, transactions);
            Console.WriteLine("Block Hash : {0}", genesisBlock.getBlockHash());
            
            // Transaction Forgery
            string[] secondTransactions = new string[]{ "DSKIM Sent 10k Bitcoins to ACS."};
            BlockHeader secondBlockheader = new BlockHeader(Encoding.UTF8.GetBytes(genesisBlock.getBlockHash()), secondTransactions);
            Block secondBlock = new Block(secondBlockheader, secondTransactions);
            Console.WriteLine("Second Block Hash : {0}", secondBlock.getBlockHash());

            // Third block
            String[] thirdTransactions = { "DSKIM Sent 500k Bitcoins to ACS." };
            BlockHeader thirdBlockheader = new BlockHeader(Encoding.UTF8.GetBytes(genesisBlock.getBlockHash()), thirdTransactions);
            Block thirdBlock = new Block(thirdBlockheader, thirdTransactions);
            Console.WriteLine("Third Block Hash : {0}", thirdBlock.getBlockHash());

            Console.Write("Any key to exit");
            Console.ReadKey();
        }
    }
}
