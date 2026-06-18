
def getSessionBySID(SID):
    import sys, win32com.client
    
    GetSession = None
    SapGuiAuto = win32com.client.GetObject("SAPGUI")
    if not type(SapGuiAuto) == win32com.client.CDispatch:
        sys.exit("Failed to retrieve object SAPGUI.")
    
    app = SapGuiAuto.GetScriptingEngine
    if not type(app) == win32com.client.CDispatch:
        SapGuiAuto = None
        sys.exit("Failed to retrieve Scripting Engine for SAPGUI.")
    
    connections = app.Connections
    if not type(connections) == win32com.client.CDispatch:
        app = None
        SapGuiAuto = None
        sys.exit("Failed to retrieve connections for SAP application.")
    
    for conn in connections:
        sessions = conn.Sessions
        for session in sessions:
            if session.Busy == False:
                if session.info.SystemName == SID:
                    GetSession = session
                    break
            else:
                continue
            break
    if not GetSession:
        sys.exit("No connections found.")
        
    return GetSession

def GetSessions():
    import sys
    import win32com.client
    from collections import defaultdict

    Dict_SAP_Connections_For_Termination ={}
    Dict_SAP_Free_Connections = {}
    Dict_SAP_Active_Connections =  defaultdict(int)
    Dict_SAP_Active_Busy_Connections =  defaultdict(int)

    SapGuiAuto = win32com.client.GetObject("SAPGUI")
    if not type(SapGuiAuto) == win32com.client.CDispatch:
        raise Exception ("No Connection found")


    application = SapGuiAuto.GetScriptingEngine
    if not type(application) == win32com.client.CDispatch:
        SapGuiAuto = None
        raise Exception ("No Connection found")
    
    connections = application.Connections
    if not type(connections) == win32com.client.CDispatch:
        application = None
        SapGuiAuto = None
        raise Exception ("No Connection found")
        
    for connection in connections:
        sessions = connection.Sessions
        for session in sessions:
            if session.Busy ==False:
                Dict_SAP_Active_Connections[session.Info.SystemName] += 1
                #if session.info.SystemName == SID:
                if session.findById("wnd[0]/usr/lblRSYST-BCODE", False) is None:
                    if session.info.SystemName not in Dict_SAP_Free_Connections:
                        Dict_SAP_Free_Connections[session.Info.SystemName] = [session]
                    else:
                        Dict_SAP_Free_Connections[session.Info.SystemName].append(session)
                    #Exit_Message = "Free Sesssions available"
                    # Exit_Message = ("Connected to session: \n"
                    #     "System Name: " + str(GetSession.Info.SystemName) + "\n"
                    #     "Transaction: " + str(GetSession.Info.Transaction) + "\n"  
                    #     "Session Number: " + str(GetSession.info.SessionNumber) + "\n"
                    #     "Client: " + str(GetSession.info.Client) + "\n"
                    #     "User: " + str(GetSession.info.User) + "\n"
                    # )
                    continue
                else:
                    if session.info.SystemName not in Dict_SAP_Connections_For_Termination:
                        Dict_SAP_Connections_For_Termination[session.Info.SystemName] = [session]
                    else:
                        Dict_SAP_Connections_For_Termination[session.Info.SystemName].append(session)
                    continue

            else:
                Dict_SAP_Active_Connections[connection.Description] += 1
                Dict_SAP_Active_Busy_Connections[connection.Description] += 1
                continue
    # if  and not GetSession and session_on_login_window != True:
    #     raise Exception ("No free Connections available")
    # elif sessions.count == 1 and not GetSession and session_on_login_window == True:
    #     Exit_Message = "Session left on login window, restarting and logging in."
    #     #raise Exception ("Session left on login window, restarting and logging in.")
    # elif not GetSession:
    #     raise Exception ("No Connection found")
    # TEST

    return (Dict_SAP_Free_Connections
            #, Exit_Message
            , Dict_SAP_Active_Connections
            , Dict_SAP_Active_Busy_Connections
            , Dict_SAP_Connections_For_Termination
    )
