package il.co.topq.appium;

import java.io.File;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.List;
import java.util.concurrent.TimeUnit;

import org.junit.After;
import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;
import org.openqa.selenium.By;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.remote.DesiredCapabilities;

import io.appium.java_client.MobileElement;
import io.appium.java_client.android.AndroidDriver;
import io.appium.java_client.remote.MobileCapabilityType;

public class CreateTaskTests {

	private static final String EMAIL = "itai.agmon@top-q.co.il";
	private static final String PASSWORD = "secret";
	private AndroidDriver<MobileElement> driver;

	@Before
	public void setUp() throws MalformedURLException {
		File apkFile = new File("Todoist_com.todoist.apk");
		DesiredCapabilities capabilities = new DesiredCapabilities();
		capabilities.setCapability(MobileCapabilityType.VERSION, "1.4.0");
		capabilities.setCapability(MobileCapabilityType.PLATFORM_NAME, "Android");
		capabilities.setCapability(MobileCapabilityType.PLATFORM_VERSION, "6.0.1");
		capabilities.setCapability(MobileCapabilityType.DEVICE_NAME, "Android");
		capabilities.setCapability(MobileCapabilityType.APP, apkFile.getAbsolutePath());
		String activity = "com.todoist.activity.HomeActivity";
		capabilities.setCapability("appPackage", "com.todoist");
		capabilities.setCapability("appActivity", activity);
		capabilities.setCapability("appWaitActivity", "com.todoist.activity.WelcomeActivity");
		capabilities.setCapability("newCommandTimeout", "600");
		
		driver = new AndroidDriver<>(new URL("http://127.0.0.1:4723/wd/hub"), capabilities);
		driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
	}

	@Test
	public void testCreateNewTask() throws InterruptedException {
		// *** Welcome Activity ***
		driver.findElement(By.id("btn_welcome_log_in")).click();

		// *** Google Play Services Activity ***
		driver.findElement(By.id("button1")).click();

		// *** Login Activity ****
		driver.findElement(By.id("log_in_email")).sendKeys(EMAIL);
		driver.findElementById("log_in_password").sendKeys(PASSWORD);
		driver.findElementById("btn_log_in").click();

		// *** Google Play Services Activity ***
		driver.findElementById("button1").click();

		// Clicking on the Today menu item
		driver.findElement(By.xpath("//android.widget.TextView[@text='Today']")).click();
		
		// Creating a new task
		driver.findElementById("fab").click();
		String taskName = "Automated task" + System.currentTimeMillis();
		WebElement message = driver.findElementById("message");
		message.sendKeys(taskName + " #work");
		driver.findElementById("button1").click();

		// Going back
		driver.findElementById("action_mode_close_button").click();
		
		

		// Searching for the newly created item
		List<MobileElement> items = driver.findElements(By.xpath("//android.widget.RelativeLayout[@resource-id='com.todoist:id/item']/android.widget.TextView"));
		boolean found = false;
		for (WebElement item : items) {
			System.out.println("Item: " + item.getText());
			if (item.getText().equals(taskName)) {
				found = true;
				break;
			}
		}
		Assert.assertTrue("Item was not created", found);
	}

	@After
	public void tearDown() {
		if (driver != null) {
			driver.quit();
		}
	}

}
