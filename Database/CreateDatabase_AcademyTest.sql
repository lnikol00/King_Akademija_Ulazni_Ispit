USE [master]
GO

CREATE DATABASE [AcademyTest]
 
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AcademyTest].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AcademyTest] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AcademyTest] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AcademyTest] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AcademyTest] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AcademyTest] SET ARITHABORT OFF 
GO
ALTER DATABASE [AcademyTest] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AcademyTest] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AcademyTest] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AcademyTest] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AcademyTest] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AcademyTest] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AcademyTest] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AcademyTest] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AcademyTest] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AcademyTest] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AcademyTest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AcademyTest] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AcademyTest] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AcademyTest] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AcademyTest] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AcademyTest] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AcademyTest] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AcademyTest] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AcademyTest] SET  MULTI_USER 
GO
ALTER DATABASE [AcademyTest] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AcademyTest] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AcademyTest] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AcademyTest] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AcademyTest] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AcademyTest] SET QUERY_STORE = OFF
GO
ALTER DATABASE [AcademyTest] SET  READ_WRITE 
GO