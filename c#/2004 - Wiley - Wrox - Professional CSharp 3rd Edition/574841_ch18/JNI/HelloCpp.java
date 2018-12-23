public class HelloCpp {

    static {
        System.load("hellocpp.so");
    }

    // this will be implemented in C++
    public native void callCpp();

    public static void main(String[] args)
    {
        System.out.println("Hello from Java!\n");

        HelloCpp cppInterface = new HelloCpp();
        cppInterface.callCpp();
    }
}
