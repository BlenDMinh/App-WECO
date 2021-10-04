using System.Collections;
using System.Collections.Generic;

public class CommunityUserData {
    public string uid;
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
    public int catagory_id; // index of user's category where this group in (default = 0)

    public Group() {
        id = "-1";
        catagory_id = -1;
    }

    public Group(string id, int cid) {
        this.id = id;
        catagory_id = cid;
    }
}
