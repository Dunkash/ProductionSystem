using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProductionSystem.Library.Test
{
    [TestFixture]
    public class RulesTest
    {

        [Test]
        public void RuleTest()
        {
            var requirements = new HashSet<string> { "dog", "cat" };
            var result = "bark";
            var explanation = "Dog barks at cat";


            var rule1 = new Rule(requirements, result, explanation);
            var rule2 = new Rule(requirements, result);

            Assert.AreEqual(rule1.Conditions, requirements);
            Assert.AreEqual(rule1.Action, result);
            Assert.AreEqual(rule1.Explanation, explanation);

            Assert.AreEqual(rule1, rule2);
        }

    }

    [TestFixture]
    public class ParserTests
    {

        [Test]
        public void ParserTestBase()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_Basic.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            Assert.AreEqual(facts.Count, 4);
            Assert.AreEqual(rules.Count, 4);

            Assert.AreEqual(facts, new HashSet<string> { "dog", "cat", "ball", "human" });
            Assert.True(rules.SetEquals(
                                        new HashSet<Rule>
                                        {
                                            new Rule(new HashSet<string> { "dog" }, "bark"),
                                            new Rule(new HashSet<string> { "dog", "cat" }, "bark"),
                                            new Rule(new HashSet<string> {"dog", "human","ball"},"playing ball"),
                                            new Rule(new HashSet<string> {"cat", "human"}, "servitude")
                                        }));
        }

        [Test]
        public void ParserTestEmpty()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_Empty.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            Assert.AreEqual(facts.Count, 0);
            Assert.AreEqual(rules.Count, 0);
        }

        [Test]
        public void ParserTestFacts()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_Facts.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            Assert.AreEqual(facts.Count, 4);
            Assert.AreEqual(rules.Count, 0);


            Assert.AreEqual(facts, new HashSet<string> { "dog", "cat", "ball", "human" });
        }

        [Test]
        public void ParserTestRules()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_Rules.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            Assert.AreEqual(facts.Count, 0);
            Assert.AreEqual(rules.Count, 4);

            Assert.True(rules.SetEquals(
                        new HashSet<Rule>
                        {
                            new Rule(new HashSet<string> { "dog" }, "bark"),
                            new Rule(new HashSet<string> { "dog", "cat" }, "bark"),
                            new Rule(new HashSet<string> {"dog", "human","ball"},"playing ball"),
                            new Rule(new HashSet<string> {"cat", "human"}, "servitude")
                        }));
        }

        [Test]
        public void ParserTestLorem()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_Lorem.txt";

            var result = Functions.Parse(file);
            var rules = result.Item2;

            Assert.AreEqual(rules.Count, 0);
        }
    }

    [TestFixture]
    class ForwardTest
    {
        [Test]
        public void ForwardBasic()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_Basic.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            facts = Functions.Forward(facts, rules);
            Assert.IsTrue(facts.SetEquals(new HashSet<string> { "dog", "cat", "ball", "human", "playing ball", "servitude", "bark" }));

            var second_iter = Functions.Forward(facts, rules);
            Assert.IsTrue(facts.SetEquals(second_iter));
        }

        [Test]
        public void ForwardEmpty()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_Empty.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            HashSet<string> first_iter = Functions.Forward(facts, rules);

            Assert.IsTrue(facts.SetEquals(first_iter));
        }

        [Test]
        public void ForwardNoRules()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_Facts.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            HashSet<string> first_iter = Functions.Forward(facts, rules);

            Assert.IsTrue(facts.SetEquals(first_iter));
        }

        [Test]
        public void ForwardNoFacts()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_Rules.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            HashSet<string> first_iter = Functions.Forward(facts, rules);

            Assert.IsTrue(facts.SetEquals(first_iter));
        }

        [Test]
        public void ForwardLorem()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_Lorem.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            HashSet<string> first_iter = Functions.Forward(facts, rules);

            Assert.IsTrue(facts.SetEquals(first_iter));
        }

        [Test]
        public void ForwardComplex()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_Complex.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            facts = Functions.Forward(facts, rules);
            Assert.IsTrue(facts.SetEquals(new HashSet<string> { "a", "b", "c" }));

            facts = Functions.Forward(facts, rules);
            Assert.IsTrue(facts.SetEquals(new HashSet<string> { "a", "b", "c", "d", "e" }));

            facts = Functions.Forward(facts, rules);
            Assert.IsTrue(facts.SetEquals(new HashSet<string> { "a", "b", "c", "d", "e", "f" }));

            var lastIter = Functions.Forward(facts, rules);
            Assert.IsTrue(facts.SetEquals(lastIter));
        }
    }

//  Эти тесты проверяют неверную имплементацию обратного поиска, и абсолютно не нужны.

    /*
  [TestFixture]
    class BackwardTest
    {
        [Test]
        public void BackwardNoResults()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_Basic.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            HashSet<string> first_iter = Functions.Backward(facts, rules);
            Assert.IsTrue(first_iter.SetEquals(new HashSet<string> { "dog", "cat", "ball", "human" }));

            var second_iter = Functions.Backward(first_iter, rules);
            Assert.IsTrue(first_iter.SetEquals(second_iter));
        }

        [Test]
        public void BackwardBasic()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_BackwardBasic.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            HashSet<string> first_iter = Functions.Backward(facts, rules);
            Assert.IsTrue(first_iter.SetEquals(new HashSet<string> { "servitude", "cat", "human" }));

            var second_iter = Functions.Backward(first_iter, rules);
            Assert.IsTrue(first_iter.SetEquals(second_iter));
        }

        [Test]
        public void BackwardEmpty()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_Empty.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            HashSet<string> first_iter = Functions.Backward(facts, rules);

            Assert.IsTrue(facts.SetEquals(first_iter));
        }

        [Test]
        public void BackwardNoRules()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_Facts.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            HashSet<string> first_iter = Functions.Backward(facts, rules);

            Assert.IsTrue(facts.SetEquals(first_iter));
        }

        [Test]
        public void BackwardNoFacts()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_Rules.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            HashSet<string> first_iter = Functions.Backward(facts, rules);

            Assert.IsTrue(facts.SetEquals(first_iter));
        }

        [Test]
        public void BackwardLorem()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_Lorem.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            HashSet<string> first_iter = Functions.Backward(facts, rules);

            Assert.IsTrue(facts.SetEquals(first_iter));
        }

        [Test]
        public void BackwardComplex()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_BackwardComplex.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            HashSet<string> firstIter = Functions.Backward(facts, rules);

            Assert.IsTrue(firstIter.SetEquals(new HashSet<string> { "a", "b", "c", "d", "e", "f" }));

            var lastIter = Functions.Backward(firstIter, rules);
            Assert.IsTrue(firstIter.SetEquals(lastIter));
        }
    }
    */
    [TestFixture]
    class EvaluateTest
    {
        [Test]
        public void ForwardEvaluateBasic()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_Basic.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            facts = Functions.Evaluate(facts, rules, true);
            Assert.IsTrue(facts.SetEquals(new HashSet<string> { "dog", "cat", "ball", "human", "playing ball", "servitude", "bark" }));

            facts = Functions.Evaluate(facts, rules, true);
            Assert.IsTrue(facts.SetEquals(new HashSet<string> { "dog", "cat", "ball", "human", "playing ball", "servitude", "bark" }));

        }

        [Test]
        public void ForwardEvaluateNoFacts()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_Rules.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            HashSet<string> first_iter = Functions.Evaluate(facts, rules, true);

            Assert.IsTrue(facts.SetEquals(first_iter));
        }

        [Test]
        public void ForwardEvaluateComplex()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_Complex.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            facts = Functions.Evaluate(facts, rules, true);
            Assert.IsTrue(facts.SetEquals(new HashSet<string> { "a", "b", "c", "d", "e", "f" }));

            var lastIter = Functions.Evaluate(facts, rules, true);
            Assert.IsTrue(facts.SetEquals(lastIter));
        }

        /*
        [Test]
        public void BackwardEvaluateNoFacts()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_Rules.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            HashSet<string> first_iter = Functions.Evaluate(facts, rules, false);

            Assert.IsTrue(facts.SetEquals(first_iter));
        }

        [Test]
        public void BackwardEvaluateBasic()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_BackwardBasic.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            HashSet<string> first_iter = Functions.Evaluate(facts, rules, false);
            Assert.IsTrue(first_iter.SetEquals(new HashSet<string> { "servitude", "cat", "human" }));

            var second_iter = Functions.Evaluate(first_iter, rules, false);
            Assert.IsTrue(first_iter.SetEquals(second_iter));
        }

        [Test]
        public void BackwardEvaluateComplex()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_BackwardComplex.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            facts = Functions.Evaluate(facts, rules, false);
            Assert.IsTrue(facts.SetEquals(new HashSet<string> { "a", "b", "c", "d", "e", "f" }));
        }
        */
    }

    [TestFixture]
    class BackwardEvaluationTest
    {
        [Test]
        public void EvaluateObvious()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_BackwardObvious.txt";

            var facts = Functions.Parse(file).Item1;

            ProductionSystem.Node res = Functions.Prove("second", facts, new HashSet<Rule> { });
            Assert.AreEqual(res.Rule, new Rule(new HashSet<string> { "second" }, "second"));
            Assert.AreEqual(res.Proves, null);
        }

        [Test]
        public void EvaluateImpossible()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_BackwardImpossible.txt";
            var facts = Functions.Parse(file).Item1;

            Node res = Functions.Prove("second", facts, new HashSet<Rule> { });
            Assert.AreEqual(res, null);
        }

        [Test]
        public void EvaluateSimple()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_BackwardSimple.txt";
            var facts = Functions.Parse(file).Item1;
            var rules = Functions.Parse(file).Item2;

            Node res = Functions.Prove("third", facts, rules);
            Assert.AreEqual(res.Rule,new Rule(new HashSet<string> {"second"},"third"));
            Assert.AreEqual(res.Proves[0].Rule, new Rule(new HashSet<string> { "first" }, "second"));
            Assert.AreEqual(res.Proves[0].Proves[0].Rule, new Rule(new HashSet<string> { "first" }, "first"));
        }

        [Test]
        public void EvaluateComplex()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_BackwardComplex.txt";
            var facts = Functions.Parse(file).Item1;
            var rules = Functions.Parse(file).Item2;

            Node res = Functions.Prove("f", facts, rules);
            Assert.AreEqual(res.Rule, new Rule(new HashSet<string> { "d", "e" }, "f"));
            Assert.AreEqual(res.Proves.Count, 2);
        }

        [Test]
        public void EvaluateComplexImpossible()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_BackwardComplexImpossible.txt";
            var facts = Functions.Parse(file).Item1;
            var rules = Functions.Parse(file).Item2;

            Node res = Functions.Prove("f", facts, rules);
            Assert.AreEqual(res, null);
        }

        [Test]
        public void EvaluateScrable()
        {
            var file = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//TestFiles//" + "test_BackwardScramble.txt";
            var facts = new HashSet<string> { "a", "b", "c" };
            var rules = Functions.Parse(file).Item2;

            Node res = Functions.Prove("cab", facts, rules);
            Assert.IsTrue(res.Rule.Action == "cab");

            res = Functions.Prove("bed", facts, rules);
            Assert.AreEqual(res, null);

            facts = new HashSet<string> { "b","e","d" };

            res = Functions.Prove("bed", facts, rules);
            Assert.IsTrue(res.Rule.Action == "bed");

            res = Functions.Prove("cab", facts, rules);
            Assert.AreEqual(res, null);

            facts = new HashSet<string> { "a", "c", "e","d" };
            res = Functions.Prove("aced",facts,rules);
            Assert.IsTrue(res.Rule.Action == "aced");

            res = Functions.Prove("bade", facts, rules);
            Assert.AreEqual(res, null);
        }


    }
}
