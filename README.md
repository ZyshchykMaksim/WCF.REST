# Self Hosted WCF REST + WebSocket #
The project has collected a basic set of WCF REST, WebSocket and logging actions. 

## Includes ##
- The inspector request and response
- Global error trapping 
- Logging of requests, responses and errors 
- Sends notifications through web-socket
- Support Cross-Origin Resource Sharing (CORS)

## Dependence ##

[websocket-sharp](https://github.com/sta/websocket-sharp "websocket-sharp")

[NLog](https://github.com/NLog/NLog "NLog")

## Configuration ##

    		<extensions>
    			<behaviorExtensions>
    				<add name="MessageInspector" type="WCF.RESTService.Inspector.MessageInspectorBehaviorExtension,WCF.RESTService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"/>
    				<add name="ServiceErrorsHandler" type="WCF.RESTService.Exception.ErrorsHandlerServiceBehaviorExtension,WCF.RESTService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"/>
    				<add name="WebSocketServer" type="WCF.RESTService.Notification.WebSocketServerExtensionElement,WCF.RESTService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"/>
    				<add name="CorsSupport" type="WCF.RESTService.Cors.CorsSupportBehaviorElement, WCF.RESTService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"/>
    			</behaviorExtensions>
    		</extensions>

    		<behaviors>
    			<serviceBehaviors>
    				<behavior name="websocket">
    					<serviceMetadata httpGetEnabled="True" httpsGetEnabled="True"/>
    					<serviceDebug includeExceptionDetailInFaults="False" />
    					<ServiceErrorsHandler IsLogFile="false"/>
    					<WebSocketServer  IsLogFile="false" Url="ws://localhost:8081/"/>
    				</behavior>
    			</serviceBehaviors>
    			<endpointBehaviors>
    				<behavior name="webHttp">
    					<webHttp />
    					<CorsSupport/>
    					<MessageInspector IsDisplayConsole="true" IsLogFile="true"/>
    				</behavior>
    			</endpointBehaviors>
    		</behaviors>
