using System.Collections;
using System.Collections.Generic;

public class CommunityUserData {
    public List<Group> groups; // Groups which user have joined
    public List<string> categories;

    // default category: Joined Groups, Your Groups

    public CommunityUserData() {
        groups = new List<Group>();
        categories = new List<string>(2){"Joined Groups", "Your Groups"};
    }
}

public class Group {
    public string id; // group index
    public int at_catagory; // index of user's category where this group in (default = 0)

    public Group() {
        id = "-1";
        at_catagory = -1;
    }

    public Group(string id, int cid) {
        this.id = id;
        at_catagory = cid;
    }
}
