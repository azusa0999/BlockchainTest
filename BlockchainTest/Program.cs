using System;
using System.Collections.Generic;
using System.Text;
using BlockchainTest.Class;
using System.Diagnostics;

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
            string[] transactions = { "Jone Sent 100 Bitcoins to Bob." };
            // 최초 노드 : genesisBlock
            BlockHeader blockheader = new BlockHeader(null, transactions);
            Block genesisBlock = new Block(blockheader, transactions);
            Console.WriteLine("Block Hash : {0}", genesisBlock.getBlockHash());

            Stopwatch stopw = new Stopwatch();//시간 측정 클래스
            Block previousBlock = genesisBlock;
            for (int i = 0; i < 5; i++)
            {
                BlockHeader secondBlockheader = new BlockHeader(Encoding.UTF8.GetBytes(previousBlock.getBlockHash()), transactions);
                Block nextBlock = new Block(secondBlockheader, transactions);
                stopw.Start();
                int count = secondBlockheader.ProofOfWorkCount();
                stopw.Stop();
                Console.WriteLine("{0} th Block Hash : {1}", i.ToString(), nextBlock.getBlockHash());
                Console.WriteLine("   └ COUNT of Proof of Work : {0} th loop", count);
                Console.WriteLine("   └ Delay : {0} millisecond", stopw.ElapsedMilliseconds);
                previousBlock = nextBlock;
                stopw.Reset();
            }
            
            Console.Write("Any key to exit");
            Console.ReadKey();
        }
    }
}
