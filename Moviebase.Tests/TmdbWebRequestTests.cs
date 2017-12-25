using Moviebase.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moviebase.Tests
{
    [TestFixture]
    public class TmdbWebRequestTests
    {
        private TmdbWebRequest _tmdbWebRequest;

        [SetUp]
        public void Setup()
        {
            _tmdbWebRequest = new TmdbWebRequest("DDD");
        }

        [Test]
        public void TestBuildQueryString()
        {
            var col = new NameValueCollection
            {
                {"ked", "dd" },
                {"kesd", "ddd" },
            };
            var result = _tmdbWebRequest.BuildQueryString(col);

            Assert.AreEqual("ked=dd&kesd=ddd", result);
        }

        [Test]
        public void TestBuildApiUri()
        {
            var col = new NameValueCollection
            {
                {"ked", "dd" },
                {"kesd", "ddd" },
            };
            var result = _tmdbWebRequest.BuildApiUri("/movie", col);

            var expected = "https://api.themoviedb.org/3/movie?api_key=DDD&ked=dd&kesd=ddd";
            Assert.AreEqual(expected, result);
        }

    }
}
