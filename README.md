# Migrately

Migrately simplifies the U.S. visa and immigration journey by combining the power of community and technology. A new easy to use process that matches and customizes the right U.S. travel, visa, and immigration process. 

Migrately is an MVP and due to the proprietary nature of the code I can only provide code snippets of my code. This repository contains SQL, .NET, and React code for the checkout feature.

<a href="https://migrately.azurewebsites.net/products">Checkout a Live Website</a>

<img src="https://wakatime.com/badge/user/77298fc6-b57e-486c-bec5-2ea798830ccd/project/1bbe3963-978f-4372-93a6-dd72fc764b9e.svg"/>


<br/>
<h1>User Not Logged In</h1>
<img src="https://github.com/EdwardLeeData/Migrately/blob/main/gif/userNotLoggedIn.gif"/>
<li>SweetAlert2 to display "Please Log In" in order to proceed to checkout page.</li>

<br/>
<h1>User Logged In User</h1>
<img src="https://github.com/EdwardLeeData/Migrately/blob/main/gif/loggedin.gif"/>
<li>Dynamic checkout page accessible for Logged in user.</li>

<br/>
<h1>Insufficient Money On Card</h1>
<img src="https://github.com/EdwardLeeData/Migrately/blob/main/gif/insufficient.gif"/>
<li>4000 0000 0000 9995</li>
<li>Payment is declined due to insufficient funds.</li>

<br/>
<h1>Card Authentication Required</h1>
<img src="https://github.com/EdwardLeeData/Migrately/blob/main/gif/authentication.gif"/>
<li>4000 0025 0000 3155</li>
<li>Payment requires authentication.</li>

<br/>
<h1>Card Accepted (Success)</h1>
<img src="https://github.com/EdwardLeeData/Migrately/blob/main/gif/success.gif"/>
<li>4242 4242 4242 4242</li>
<li>Payment succeeds.</li>
<li>Dyanmic checkout --> processing page --> success page</li>
<li>Executed all back-end components in the processing page with 2 seconds timeout after last API finishes.</li>

<br/>
<h1>Downloading Invoice PDF</h1>
<img src="https://github.com/EdwardLeeData/Migrately/blob/main/gif/downloadInvoice.gif"/>
<li>Customer can click on the "Download Invoice" to download the pdf.</li>

<br/>
<h1>Clicking "Pay Online"</h1>
<img src="https://github.com/EdwardLeeData/Migrately/blob/main/gif/clickInvoice.gif"/>
<li>Click "Pay Online" to populate online payment portal.</li>
<li>Click "View invoice details" to populate a side panel that breaksdown payment.</li>

<br/>
<h1>Download Receipt</h1>
<img src="https://github.com/EdwardLeeData/Migrately/blob/main/gif/receipt.gif"/>
<li>Once invoice is paid, receipt is available for the download.</li>

<br/>
<h1>Returning to Products Page</h1>
<img src="https://github.com/EdwardLeeData/Migrately/blob/main/gif/currentPlan.gif"/>
<li>Returning to products page will display a ribbon on the customer's current plan.</li>
<li>Display next billing cycle under the toggle button in the center.</li>

<br/>
<h1>Updating Plan</h1>
<img src="https://github.com/EdwardLeeData/Migrately/blob/main/gif/changePlan.gif"/>
<li>Customer can update their current plan.</li>
<li>Credit card information is stored for the existing customer.</li>

