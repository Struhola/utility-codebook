
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