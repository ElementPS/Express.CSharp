# Express.CSharp
Integration to Element Express API using CSharp

* Questions?  certification@elementps.com
* **Feature request?** Open an issue.
* Feel like **contributing**?  Submit a pull request.

##Overview

This repository demonstrates an integration to the Element Express API using c#.  The code was compiled and tested using Microsoft Visual Studio Express 2013 for Windows Desktop.  The application itself allows a user to populate a credit sale request or a health check request using either SOAP or XML formats and then send the request to Element's Express API for further processing.  The app then displays the result returned from Element's platform.

![Express.CSharp](https://github.com/ElementPS/Express.CSharp/blob/master/screenshot.PNG)

##Prerequisites

Please contact your Integration Analyst for any questions about the prerequisite below.

* Create Express test account: http://www.elementps.com/Resources/Create-a-Test-Account

##Documentation/Troubleshooting

* When you create your Express test account an email will be sent containing links to documentation.

##Step 1: Generate a request

You can either generate an XML request or a SOAP request.  The Credentials and Application elements are empty below because these elements are read from the App.config file.  When you receive an email after creating your test account the email will contain the information necessary to populate these fields in the App.config.  Only the credit sale request is shown below, please take a look at the code for the health check message.

This is the XML request:

```
<CreditCardSale xmlns="https://transaction.elementexpress.com">
  <Credentials>
    <AccountID></AccountID>
    <AccountToken></AccountToken>
    <AcceptorID></AcceptorID>
  </Credentials>
  <Application>
    <ApplicationID></ApplicationID>
    <ApplicationVersion>1.0</ApplicationVersion>
    <ApplicationName>Express.CSharp</ApplicationName>
  </Application>
  <Terminal>
    <TerminalID>01</TerminalID>
    <CardholderPresentCode>2</CardholderPresentCode>
    <CardInputCode>5</CardInputCode>
    <TerminalCapabilityCode>3</TerminalCapabilityCode>
    <TerminalEnvironmentCode>2</TerminalEnvironmentCode>
    <CardPresentCode>2</CardPresentCode>
    <MotoECICode>1</MotoECICode>
    <CVVPresenceCode>1</CVVPresenceCode>
  </Terminal>
  <Transaction>
    <TransactionAmount>6.55</TransactionAmount>
    <MarketCode>7</MarketCode>
  </Transaction>
  <Card>
    <CardNumber>5499990123456781</CardNumber>
    <ExpirationMonth>12</ExpirationMonth>
    <ExpirationYear>19</ExpirationYear>
  </Card>
</CreditCardSale>

```

And this is the SOAP request:


```
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <CreditCardSale xmlns="https://transaction.elementexpress.com">
      <credentials>
        <AccountID></AccountID>
        <AccountToken></AccountToken>
        <AcceptorID></AcceptorID>
      </credentials>
      <application>
        <ApplicationID></ApplicationID>
        <ApplicationVersion>1.0</ApplicationVersion>
        <ApplicationName>Express.CSharp</ApplicationName>
      </application>
      <terminal>
        <TerminalID>01</TerminalID>
        <CardholderPresentCode>Present</CardholderPresentCode>
        <CardInputCode>ManualKeyed</CardInputCode>
        <TerminalCapabilityCode>MagstripeReader</TerminalCapabilityCode>
        <TerminalEnvironmentCode>LocalAttended</TerminalEnvironmentCode>
        <CardPresentCode>Present</CardPresentCode>
        <MotoECICode>NotUsed</MotoECICode>
        <CVVPresenceCode>NotProvided</CVVPresenceCode>
      </terminal>
      <card>
        <CardNumber>5499990123456781</CardNumber>
        <ExpirationMonth>12</ExpirationMonth>
        <ExpirationYear>19</ExpirationYear>
      </card>
      <transaction>
        <TransactionAmount>6.55</TransactionAmount>
        <MarketCode>Retail</MarketCode>
      </transaction>
    </CreditCardSale>
  </soap:Body>
</soap:Envelope>
```

##Step 2: Send Request to the Express API

The HttpSender class is used to send both the XML and SOAP requests.  The only difference is the endpoint the data is sent to and the SOAPAction header that is added when sending SOAP requests.

```
HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
if (!String.IsNullOrEmpty(action))
{
  webRequest.Headers.Add("SOAPAction", action);
}

webRequest.ContentType = "text/xml;charset=\"utf-8\"";
webRequest.Accept = "text/xml";
webRequest.Method = "POST";
webRequest.ContentLength = data.Length;

using (var stream = webRequest.GetRequestStream())
{
  stream.Write(byteData, 0, byteData.Length);
}
```

##Step 3: Receive response from Express API

The response will be in an XML format regardless of sending XML or SOAP but the actual data returned will be slightly different.  This is the response to an XML request.

```
<?xml version="1.0" encoding="utf-16"?>
<CreditCardSaleResponse xmlns="https://transaction.elementexpress.com">
 <Response>
  <ExpressResponseCode>0</ExpressResponseCode>
  <ExpressResponseMessage>Approved</ExpressResponseMessage>
  <HostResponseCode>000</HostResponseCode>
  <HostResponseMessage>AP</HostResponseMessage>
  <ExpressTransactionDate>20150518</ExpressTransactionDate>
  <ExpressTransactionTime>121654</ExpressTransactionTime>
  <ExpressTransactionTimezone>UTC-05:00:00</ExpressTransactionTimezone>
  <Batch>
   <HostBatchID>1</HostBatchID>
   <HostItemID>8</HostItemID>
   <HostBatchAmount>52.40</HostBatchAmount>
  </Batch>
  <Card>
   <AVSResponseCode>N</AVSResponseCode>
   <CardLogo>Mastercard</CardLogo>
  </Card>
  <Transaction>
   <TransactionID>2005019034</TransactionID>
   <ApprovalNumber>000016</ApprovalNumber>
   <AcquirerData>bMCC1440300714</AcquirerData>
   <ProcessorName>NULL_PROCESSOR_TEST</ProcessorName>
   <TransactionStatus>Approved</TransactionStatus>
   <TransactionStatusCode>1</TransactionStatusCode>
   <ApprovedAmount>6.55</ApprovedAmount>
  </Transaction>
 </Response>
</CreditCardSaleResponse>
```

This is the response to a SOAP request

```
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
 <soap:Body>
  <CreditCardSaleResponse xmlns="https://transaction.elementexpress.com">
   <response>
    <ExpressResponseCode>0</ExpressResponseCode>
    <ExpressResponseMessage>Approved</ExpressResponseMessage>
    <ExpressTransactionDate>20150518</ExpressTransactionDate>
    <ExpressTransactionTime>121747</ExpressTransactionTime>
    <ExpressTransactionTimezone>UTC-05:00</ExpressTransactionTimezone>
    <HostResponseCode>000</HostResponseCode>
    <HostResponseMessage>AP</HostResponseMessage>
    <Credentials />
    <Batch>
     <BatchCloseType>Regular</BatchCloseType>
     <BatchQueryType>Totals</BatchQueryType>
     <HostBatchID>1</HostBatchID>
     <HostItemID>9</HostItemID>
     <HostBatchAmount>58.95</HostBatchAmount>
     <BatchGroupingCode>FullBatch</BatchGroupingCode>
     <BatchIndexCode>Current</BatchIndexCode>
    </Batch>
    <Card>
     <EncryptedFormat>Default</EncryptedFormat>
     <AVSResponseCode>N</AVSResponseCode>
     <CardLogo>Mastercard</CardLogo>
    </Card>
    <Transaction>
     <TransactionID>2005019038</TransactionID>
     <ApprovalNumber>000017</ApprovalNumber>
     <ReversalType>System</ReversalType>
     <MarketCode>Default</MarketCode>
     <AcquirerData>bMCC1440300714</AcquirerData>
     <BillPaymentFlag>False</BillPaymentFlag>
     <DuplicateCheckDisableFlag>False</DuplicateCheckDisableFlag>
     <DuplicateOverrideFlag>False</DuplicateOverrideFlag>
     <RecurringFlag>False</RecurringFlag>
     <ProcessorName>NULL_PROCESSOR_TEST</ProcessorName>
     <TransactionStatus>Approved</TransactionStatus>
     <TransactionStatusCode>1</TransactionStatusCode>
     <PartialApprovedFlag>False</PartialApprovedFlag>
     <ApprovedAmount>6.55</ApprovedAmount>
    </Transaction>
    <PaymentAccount>
     <PaymentAccountType>CreditCard</PaymentAccountType>
     <PASSUpdaterBatchStatus>Null</PASSUpdaterBatchStatus>
     <PASSUpdaterOption>Null</PASSUpdaterOption>
    </PaymentAccount>
    <Address />
    <ScheduledTask>
     <RunFrequency>OneTimeFuture</RunFrequency>
     <RunUntilCancelFlag>False</RunUntilCancelFlag>
     <ScheduledTaskStatus>Active</ScheduledTaskStatus>
    </ScheduledTask>
    <DemandDepositAccount>
     <DDAAccountType>Checking</DDAAccountType>
     <CheckType>Personal</CheckType>
    </DemandDepositAccount>
    <TransactionSetup>
     <TransactionSetupMethod>Null</TransactionSetupMethod>
     <Device>Null</Device>
     <Embedded>False</Embedded>
     <CVVRequired>False</CVVRequired>
     <AutoReturn>False</AutoReturn>
     <DeviceInputCode>NotUsed</DeviceInputCode>
    </TransactionSetup>
    <Terminal>
     <TerminalType>Unknown</TerminalType>
     <CardPresentCode>UseDefault</CardPresentCode>
     <CardholderPresentCode>UseDefault</CardholderPresentCode>
     <CardInputCode>UseDefault</CardInputCode>
     <CVVPresenceCode>UseDefault</CVVPresenceCode>
     <TerminalCapabilityCode>UseDefault</TerminalCapabilityCode>
     <TerminalEnvironmentCode>UseDefault</TerminalEnvironmentCode>
     <MotoECICode>UseDefault</MotoECICode>
     <CVVResponseType>Regular</CVVResponseType>
     <ConsentCode>NotUsed</ConsentCode>
     <TerminalEncryptionFormat>Default</TerminalEncryptionFormat>
    </Terminal>
    <AutoRental>
     <AutoRentalVehicleClassCode>Unused</AutoRentalVehicleClassCode>
     <AutoRentalDistanceUnit>Unused</AutoRentalDistanceUnit>
     <AutoRentalAuditAdjustmentCode>NoAdjustments</AutoRentalAuditAdjustmentCode>
    </AutoRental>
    <Healthcare>
     <HealthcareFlag>False</HealthcareFlag>
     <HealthcareFirstAccountType>NotSpecified</HealthcareFirstAccountType>
     <HealthcareFirstAmountType>LedgerBalance</HealthcareFirstAmountType>
     <HealthcareFirstAmountSign>Positive</HealthcareFirstAmountSign>
     <HealthcareSecondAccountType>NotSpecified</HealthcareSecondAccountType>
     <HealthcareSecondAmountType>LedgerBalance</HealthcareSecondAmountType>
     <HealthcareSecondAmountSign>Positive</HealthcareSecondAmountSign>
     <HealthcareThirdAccountType>NotSpecified</HealthcareThirdAccountType>
     <HealthcareThirdAmountType>LedgerBalance</HealthcareThirdAmountType>
     <HealthcareThirdAmountSign>Positive</HealthcareThirdAmountSign>
     <HealthcareFourthAccountType>NotSpecified</HealthcareFourthAccountType>
     <HealthcareFourthAmountType>LedgerBalance</HealthcareFourthAmountType>
     <HealthcareFourthAmountSign>Positive</HealthcareFourthAmountSign>
    </Healthcare>
    <Lodging>
     <LodgingPrestigiousPropertyCode>NonParticipant</LodgingPrestigiousPropertyCode>
     <LodgingSpecialProgramCode>Default</LodgingSpecialProgramCode>
     <LodgingChargeType>Default</LodgingChargeType>
    </Lodging>
    <BIN />
    <EnhancedBIN />
   </response>
  </CreditCardSaleResponse>
 </soap:Body>
</soap:Envelope>
```


##Step 4: Parse response data

You may parse the XML response in any manner but one way is to load the returned XML direclty into an XmlDocument and then perform XPath queries against that XmlDocument.

```
var xmlDoc = new XmlDocument();
xmlDoc.LoadXml(response);
```


###©2014-2015 Element Payment Services, Inc., a Vantiv company. All Rights Reserved.

Disclaimer:
This software and all specifications and documentation contained herein or provided to you hereunder (the "Software") are provided free of charge strictly on an "AS IS" basis. No representations or warranties are expressed or implied, including, but not limited to, warranties of suitability, quality, merchantability, or fitness for a particular purpose (irrespective of any course of dealing, custom or usage of trade), and all such warranties are expressly and specifically disclaimed. Element Payment Services, Inc., a Vantiv company, shall have no liability or responsibility to you nor any other person or entity with respect to any liability, loss, or damage, including lost profits whether foreseeable or not, or other obligation for any cause whatsoever, caused or alleged to be caused directly or indirectly by the Software. Use of the Software signifies agreement with this disclaimer notice.


![Analytics](https://ga-beacon.appspot.com/UA-60858025-40/Express.CSharp/readme?pixel)


