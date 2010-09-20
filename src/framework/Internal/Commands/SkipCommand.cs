﻿// ***********************************************************************
// Copyright (c) 2010 Charlie Poole
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System;
using NUnit.Framework.Api;

namespace NUnit.Framework.Internal
{
    public class SkipCommand : TestCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SkipCommand"/> class.
        /// </summary>
        /// <param name="test">The test being skipped.</param>
        public SkipCommand(Test test) : base(test)
        {
        }

        /// <summary>
        /// Overridden to simply set the CurrentResult to the
        /// appropriate Skipped state
        /// </summary>
        public override TestResult Execute(object TestObject)
        {
            TestResult testResult = new TestResult(Test);

            switch (Test.RunState)
            {
                default:
                case RunState.Skipped:
                    testResult.SetResult(ResultState.Skipped, Test.SkipReason);
                    break;
                case RunState.Ignored:
                    testResult.SetResult(ResultState.Ignored, Test.SkipReason);
                    break;
                case RunState.NotRunnable:
                    if (Test.BuilderException != null)
                        testResult.SetResult(ResultState.NotRunnable,
                            ExceptionHelper.BuildMessage(Test.BuilderException),
                            ExceptionHelper.BuildStackTrace(Test.BuilderException));
                    else
                        testResult.SetResult(ResultState.NotRunnable, Test.SkipReason);
                    break;
            }

            return testResult;
        }
    }
}