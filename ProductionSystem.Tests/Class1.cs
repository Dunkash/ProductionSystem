using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


            var rule1 = new ProductionSystem.Rule(requirements, result, explanation);
            var rule2 = new ProductionSystem.Rule(requirements, result);

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
            var file = AppDomain.CurrentDomain.BaseDirectory + "test_Basic.txt";

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
            var file = AppDomain.CurrentDomain.BaseDirectory + "test_Empty.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            Assert.AreEqual(facts.Count, 0);
            Assert.AreEqual(rules.Count, 0);
        }

        [Test]
        public void ParserTestFacts()
        {
            var file = AppDomain.CurrentDomain.BaseDirectory + "test_Facts.txt";

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
            var file = AppDomain.CurrentDomain.BaseDirectory + "test_Rules.txt";

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
            var file = AppDomain.CurrentDomain.BaseDirectory + "test_Lorem.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
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
            var file = AppDomain.CurrentDomain.BaseDirectory + "test_Basic.txt";

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
            var file = AppDomain.CurrentDomain.BaseDirectory + "test_Empty.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            HashSet<string> first_iter = Functions.Forward(facts, rules);

            Assert.IsTrue(facts.SetEquals(first_iter));
        }

        [Test]
        public void ForwardNoRules()
        {
            var file = AppDomain.CurrentDomain.BaseDirectory + "test_Facts.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            HashSet<string> first_iter = Functions.Forward(facts, rules);

            Assert.IsTrue(facts.SetEquals(first_iter));
        }

        [Test]
        public void ForwardNoFacts()
        {
            var file = AppDomain.CurrentDomain.BaseDirectory + "test_Rules.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            HashSet<string> first_iter = Functions.Forward(facts, rules);

            Assert.IsTrue(facts.SetEquals(first_iter));
        }

        [Test]
        public void ForwardLorem()
        {
            var file = AppDomain.CurrentDomain.BaseDirectory + "test_Lorem.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            HashSet<string> first_iter = Functions.Forward(facts, rules);

            Assert.IsTrue(facts.SetEquals(first_iter));
        }

        [Test]
        public void ForwardComplex()
        {
            var file = AppDomain.CurrentDomain.BaseDirectory + "test_Complex.txt";

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

    [TestFixture]
    class BackwardTest
    {
        [Test]
        public void BackwardNoResults()
        {
            var file = AppDomain.CurrentDomain.BaseDirectory + "test_Basic.txt";

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
            var file = AppDomain.CurrentDomain.BaseDirectory + "test_BackwardBasic.txt";

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
            var file = AppDomain.CurrentDomain.BaseDirectory + "test_Empty.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            HashSet<string> first_iter = Functions.Backward(facts, rules);

            Assert.IsTrue(facts.SetEquals(first_iter));
        }

        [Test]
        public void BackwardNoRules()
        {
            var file = AppDomain.CurrentDomain.BaseDirectory + "test_Facts.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            HashSet<string> first_iter = Functions.Backward(facts, rules);

            Assert.IsTrue(facts.SetEquals(first_iter));
        }

        [Test]
        public void BackwardNoFacts()
        {
            var file = AppDomain.CurrentDomain.BaseDirectory + "test_Rules.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            HashSet<string> first_iter = Functions.Backward(facts, rules);

            Assert.IsTrue(facts.SetEquals(first_iter));
        }

        [Test]
        public void BackwardLorem()
        {
            var file = AppDomain.CurrentDomain.BaseDirectory + "test_Lorem.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            HashSet<string> first_iter = Functions.Backward(facts, rules);

            Assert.IsTrue(facts.SetEquals(first_iter));
        }

        public void BackwardComplex()
        {
            var file = AppDomain.CurrentDomain.BaseDirectory + "test_BacwardComplex.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            HashSet<string> first_iter = Functions.Backward(facts, rules);

            facts = Functions.Backward(facts, rules);
            Assert.IsTrue(facts.SetEquals(new HashSet<string> { "a", "b", "c", "d", "e", "f" }));

            var lastIter = Functions.Backward(facts, rules);
            Assert.IsTrue(facts.SetEquals(lastIter));
        }
    }

    [TestFixture]
    class EvaluateTest
    {
        [Test]
        public void ForwardEvaluateBasic()
        {
            var file = AppDomain.CurrentDomain.BaseDirectory + "test_Basic.txt";

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
            var file = AppDomain.CurrentDomain.BaseDirectory + "test_Rules.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            HashSet<string> first_iter = Functions.Evaluate(facts, rules, true);

            Assert.IsTrue(facts.SetEquals(first_iter));
        }

        [Test]
        public void ForwardEvaluateComplex()
        {
            var file = AppDomain.CurrentDomain.BaseDirectory + "test_Complex.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            facts = Functions.Evaluate(facts, rules, true);
            Assert.IsTrue(facts.SetEquals(new HashSet<string> { "a", "b", "c", "d", "e", "f" }));

            var lastIter = Functions.Evaluate(facts, rules, true);
            Assert.IsTrue(facts.SetEquals(lastIter));
        }

        [Test]
        public void BackwardEvaluateNoFacts()
        {
            var file = AppDomain.CurrentDomain.BaseDirectory + "test_Rules.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            HashSet<string> first_iter = Functions.Evaluate(facts, rules, false);

            Assert.IsTrue(facts.SetEquals(first_iter));
        }

        [Test]
        public void BackwardEvaluateBasic()
        {
            var file = AppDomain.CurrentDomain.BaseDirectory + "test_BackwardBasic.txt";

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
            var file = AppDomain.CurrentDomain.BaseDirectory + "test_BackwardComplex.txt";

            var result = Functions.Parse(file);
            var facts = result.Item1;
            var rules = result.Item2;

            facts = Functions.Evaluate(facts, rules, false);
            Assert.IsTrue(facts.SetEquals(new HashSet<string> { "a", "b", "c", "d", "e", "f" }));
        }
    }
}
