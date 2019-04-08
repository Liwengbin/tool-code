public static String UnderlineToHump(String name) {
    String spl = "_";
    String[] n = name.split("");
    StringBuilder sname = new StringBuilder();
    if (name.contains(spl)) {
        for (int i = 0; i < n.length; i++) {
            if (spl.equals(n[i])) {
                i++;
                n[i] = n[i].toUpperCase();
            }
            sname.append(n[i]);
        }
    }
    return sname.toString();
}

public static String HumpToUnderline(String name) {
    String spl = "_";
    int temp = 0;
    StringBuilder sname = new StringBuilder(name);

    if (!name.contains(spl)) {
        for (int i = 0; i < name.length(); i++) {
            if (Character.isUpperCase(name.charAt(i))) {
                sname.insert(i + temp, spl);
                temp++;
            }
        }
    }
    return sname.toString().toLowerCase();
}
