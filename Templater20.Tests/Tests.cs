using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Templater20.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestSimpleVariable()
        {
            var template = "<body>variable {{someVar}} </body>";
            var engine = new Engine();

            var data = new ViewBag();
            data["someVar"] = "10";

            var ret = engine.Render(template, data);

            Assert.AreEqual("<body>variable 10 </body>", ret);
        }


        [TestMethod]
        public void TestOneLevelIfEvalsToTrue()
        {
            var template = "<body> {% if a = 1 %} should see this {% endif %} </body>";
            var engine = new Engine();

            var data = new ViewBag();
            data["a"] = "1";

            var ret = engine.Render(template, data);

            Assert.AreEqual("<body> should see this </body>", ret);
        }

        [TestMethod]
        public void TestOneLevelIfEvalsToFalse()
        {
            var template = "<body> {% if a = 22 %} should NOT see this {% endif %} </body>";
            var engine = new Engine();

            var data = new ViewBag();
            data["a"] = "1";

            var ret = engine.Render(template, data);

            Assert.AreEqual("<body>  </body>", ret);
        }

        [TestMethod]
        public void TestTwoLevelIfEvalsToTrue()
        {
            var template = "<body> {% if a = 1 %} level1 {%if b = 2 %} level2 {% endif %} {% endif %} </body>";
            var engine = new Engine();

            var data = new ViewBag();
            data["a"] = "1";
            data["b"] = "2";

            var ret = engine.Render(template, data);

            Assert.AreEqual("<body> level1 level2 </body>", ret);
        }

        [TestMethod]
        public void TestTwoLevelIfEvalsToFalse()
        {
            var template = "<body> {% if a = 1 %} level1 {%if b = 33 %} level2 {% endif %} {% endif %} </body>";
            var engine = new Engine();

            var data = new ViewBag();
            data["a"] = "1";
            data["b"] = "2";

            var ret = engine.Render(template, data);

            Assert.AreEqual("<body> level1 </body>", ret);
        }


        [TestMethod]
        public void TestOneLevelLoop()
        {
            var template = "<body>{% for it in items %} iteration {{it}}{% endfor %}</body>";
            var engine = new Engine();

            var data = new ViewBag();
            data["items"] = new List<int> {1,2,3};

            var ret = engine.Render(template, data);

            Assert.AreEqual("<body> iteration 1 iteration 2 iteration 3</body>",ret);
        }

        [TestMethod]
        public void TestTwoLevelLoop()
        {
            var template = "<body>{% for it in items %}{% for p in items2 %} it {{p}}{% endfor %}{% endfor %}</body>";
            var engine = new Engine();

            var data = new ViewBag();
            data["items"] = new List<int> { 1, 2 };
            data["items2"] = new List<int> { 3, 4 };

            var ret = engine.Render(template, data);

            Assert.AreEqual("<body> it 3 it 4 it 3 it 4</body>", ret);
        }


        [TestMethod]
        public void TestIfInsideLoop()
        {
            var template = "<body>{% for it in items %}{% if it = 2 %} insideIf {{it}} {% endif %}{% endfor %}</body>";
            var engine = new Engine();

            var data = new ViewBag();
            data["items"] = new List<int> {1, 2};

            var ret = engine.Render(template, data);

            Assert.AreEqual("<body>insideIf 2</body>", ret);
        }

        [TestMethod]
        public void TestLoopByListOfClasses()
        {
            var template = "{% for p in persons %}{{p.Name}} {% endfor %}";
            var engine = new Engine();

            var persons = new List<Person>();
            persons.Add(new Person {Name = "John"});
            persons.Add(new Person { Name = "Jimmy" });

            var data = new ViewBag();
            data["persons"] = persons;

            var ret = engine.Render(template, data);

            Assert.AreEqual("John Jimmy", ret.Trim());
        }

        [TestMethod]
        public void TestIfWithObject()
        {
            var template = "{% if person.Office = 10 %} {{person.Name}} {% endif %}{% if person.Office = 20 %} {{person.Name}} {% endif %}";
            var person = new Person {Name = "Jimmy", Office = 10};

            var engine = new Engine();

            var data = new ViewBag();
            data["person"] = person;

            var ret = engine.Render(template, data);

            Assert.AreEqual("Jimmy", ret);
        }
    }

    

    public class Person
    {
        public string Name;
        public int Office;
    }
}
