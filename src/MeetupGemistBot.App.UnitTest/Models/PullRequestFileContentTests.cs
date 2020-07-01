using FluentAssertions;
using MeetupGemistBot.App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MeetupGemistBot.App.UnitTest.Models
{
    public class PullRequestFileContentTests
    {
        [Fact]
        public void GivenFileContentDoesNotContainTitleAndYouTubeIdFields_WhenMatchIsDetermined_ThenIsMatchShouldReturnFalse()
        {
            // Arrange
            var fileContent = GetInvalidFileWithoutTitleAndYouTubeId();

            // Act
            var pullRequestFileContent = new PullRequestFileContent(fileContent);

            // Assert
            pullRequestFileContent.IsMatch.Should().BeFalse();
        }

        [Fact]
        public void GivenFileContentDoesNotContainTitleField_WhenMatchIsDetermined_ThenIsMatchShouldReturnFalse()
        {
            // Arrange
            var fileContent = GetInvalidFileWithoutTitle();

            // Act
            var pullRequestFileContent = new PullRequestFileContent(fileContent);

            // Assert
            pullRequestFileContent.IsMatch.Should().BeFalse();
        }

        [Fact]
        public void GivenFileContentDoesNotContainTitleValue_WhenMatchIsDetermined_ThenIsMatchShouldReturnFalse()
        {
            // Arrange
            var fileContent = GetInValidFileWithoutTitleValue();

            // Act
            var pullRequestFileContent = new PullRequestFileContent(fileContent);

            // Assert
            pullRequestFileContent.IsMatch.Should().BeFalse();
        }

        [Fact]
        public void GivenFileContentDoesNotContainYouTubeIdField_WhenMatchIsDetermined_ThenIsMatchShouldReturnFalse()
        {
            // Arrange
            var fileContent = GetInvalidFileWithoutYouTubeId();

            // Act
            var pullRequestFileContent = new PullRequestFileContent(fileContent);

            // Assert
            pullRequestFileContent.IsMatch.Should().BeFalse();
        }

        [Fact]
        public void GivenFileContentContainsTitleAndYouTubeId_WhenMatchIsDetermined_ThenIsMatchShouldReturnTrue()
        {
            // Arrange
            var fileContent = GetValidFile();

            // Act
            var pullRequestFileContent = new PullRequestFileContent(fileContent);

            // Assert
            pullRequestFileContent.IsMatch.Should().BeTrue();
        }


        private static string GetValidFile()
        {
            return @"
---
title: Online Virtual SDN event
youtube_id: uDeTxhx1QQo
date: 2020-06-09
category: dev
tags: [SDN, Serverless, microservices, messaging, azure]
---

Online Virtual SDN event 9 june 2020

Getting real-time insights from your serverless solution - Eduard Keilholz
From a distributed monolith to a microservices solution - Jan de Vries

Met dank aan onze corporate sponsoren Achmea, Microsoft en de onze SDN leden.";
        }

        private static string GetInValidFileWithoutTitleValue()
        {
            return @"
---
title: 
youtube_id: uDeTxhx1QQo
date: 2020-06-09
category: dev
tags: [SDN, Serverless, microservices, messaging, azure]
---

Online Virtual SDN event 9 june 2020

Getting real-time insights from your serverless solution - Eduard Keilholz
From a distributed monolith to a microservices solution - Jan de Vries

Met dank aan onze corporate sponsoren Achmea, Microsoft en de onze SDN leden.";
        }

        private static string GetInvalidFileWithoutTitleAndYouTubeId()
        {
            return @"
---
date: 2020-06-09
category: dev
tags: [SDN, Serverless, microservices, messaging, azure]
---

Online Virtual SDN event 9 june 2020

Getting real-time insights from your serverless solution - Eduard Keilholz
From a distributed monolith to a microservices solution - Jan de Vries

Met dank aan onze corporate sponsoren Achmea, Microsoft en de onze SDN leden.";
        }

        private static string GetInvalidFileWithoutTitle()
        {
            return @"
---
youtube_id: uDeTxhx1QQo
date: 2020-06-09
category: dev
tags: [SDN, Serverless, microservices, messaging, azure]
---

Online Virtual SDN event 9 june 2020

Getting real-time insights from your serverless solution - Eduard Keilholz
From a distributed monolith to a microservices solution - Jan de Vries

Met dank aan onze corporate sponsoren Achmea, Microsoft en de onze SDN leden.";
        }

        private string GetInvalidFileWithoutYouTubeId()
        {
            return @"
---
title: Online Virtual SDN event
date: 2020-06-09
category: dev
tags: [SDN, Serverless, microservices, messaging, azure]
---

Online Virtual SDN event 9 june 2020

Getting real-time insights from your serverless solution - Eduard Keilholz
From a distributed monolith to a microservices solution - Jan de Vries

Met dank aan onze corporate sponsoren Achmea, Microsoft en de onze SDN leden.";
        }
    }
}
