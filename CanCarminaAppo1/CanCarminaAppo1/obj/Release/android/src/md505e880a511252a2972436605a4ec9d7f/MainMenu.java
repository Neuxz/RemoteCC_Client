package md505e880a511252a2972436605a4ec9d7f;


public class MainMenu
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("CanCarminaAppo1.MainMenu, CanCarminaAppo1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MainMenu.class, __md_methods);
	}


	public MainMenu () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MainMenu.class)
			mono.android.TypeManager.Activate ("CanCarminaAppo1.MainMenu, CanCarminaAppo1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
