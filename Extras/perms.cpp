#include <bits/stdc++.h>
using namespace std;

#define faster ios_base::sync_with_stdio(false),cin.tie(NULL),cout.tie(NULL)
#define mp make_pair
#define pb push_back
#define ff first
#define ss second

typedef long long ll;
typedef pair<int, int> pii;
typedef vector<int> vi;

const int MOD = 1e9 + 7;
const int INF = 1e9 + 5;
const int MAXN = 1e5+13;
const ll LINF = LLONG_MAX;
ll powmod (ll a,ll b) {ll res=1;a%=MOD; assert(b>=0); for(;b;b>>=1){if(b&1)res=res*a%MOD;a=a*a%MOD;}return res;}

set<int> allValid;
set<int> allValidRemoveSize;

void CheckValid(int grid[][4]) {
	set<pair<int, int> > toRemove;

	//Row check
	for (int i = 0; i < 4; ++i)
    {
        int filledInRow = 0;
        int rowFillType = -1;
        for (int j = 0; j < 4; ++j)
        {
            if (grid[i][j] == 0)
            {
                break;
            }                        
            filledInRow++;
        }

        if (filledInRow == 4)
        {
            for (int j = 0; j < 4; ++j)
            {
                toRemove.insert({i, j});
            }
        }
    }

    //Column check
    for (int j = 0; j < 4; ++j)
    {
        int filledInColumn = 0;
        int colFillType = -1;
        for (int i = 0; i < 4; ++i)
        {
            if (grid[i][j] == 0)
            {
                break;
            }            
            filledInColumn++;
        }

        if (filledInColumn == 4)
        {
            for (int i = 0; i < 4; ++i)
            {
                toRemove.insert({i, j});
            }
        }
    }

    //Diagonal check 1     
    for (int i = 0, colFillType = -1; i < 4; ++i)
    {
        if (grid[i][i] == 0)
        {
            break;
        }        

        //all diagonal elements match
        if (i == 4 - 1)
        {
            for (int j = 0; j < 4; ++j)
            {
                toRemove.insert({j, j});
            }
        }
    }

    //Diagonal check 2     
    for (int i = 0, colFillType = -1; i < 4; ++i)
    {
        if (grid[i][3-i] == 0)
        {
            break;
        }

        //all diagonal elements match
        if (i == 4 - 1)
        {
            for (int j = 0; j < 4; ++j)
            {
                toRemove.insert({j, 3-j});
            }
        }
    }

    if (toRemove.empty() || toRemove.size() != 13) {
    	return;
    }

    int key = 0, cur = 1;

    for (int i = 0; i < 4; ++i) {
    	for (int j = 0; j < 4; ++j) {

    		if (toRemove.find({i, j}) != toRemove.end()) {
    			key += cur;    			
    		}
    		else {    			
    		}
    		cur = (cur<<1);
    	}    	
    }

    if (allValid.find(key) == allValid.end()) {
    	allValid.insert(key);
	    allValidRemoveSize.insert((int)toRemove.size());
    }    
}

void PrintGrid (int key) {
	for (int i = 0; i < 4; ++i) {
    	for (int j = 0; j < 4; ++j) {
    		cout << (key&1) << ' ';
    		key = (key>>1);
    	}
    	cout << '\n';
    }
    cout << '\n';
}

int Bits(int x) {
	int res = 0;

	while (x) {
		res += (x&1 == 1);
		x >>= 1;
	}

	return res;
}

bool comp(int a, int b) {
	return Bits(a) < Bits(b);
}

int main() {
	faster;

	int grid[4][4] = {};
	int totalWays = 0;

	for (int x = 0; x < (1<<16); ++x) {
		int cur = x;

		for (int i = 0; i < 4; ++i) {
			for (int j = 0; j < 4; ++j){
				grid[i][j] = cur&1;
				cur = (cur>>1);
			}
		}
		CheckValid(grid);
	}

	vector<int> keys;
	for (auto key: allValid) {
		keys.push_back(key);
	}
	sort(keys.begin(), keys.end(), comp);
	for (auto key: keys) {
		cout << Bits(key) << '\n';
		PrintGrid(key);
	}

	cout << allValid.size() << ' ' << allValidRemoveSize.size() << '\n';
	for (auto x : allValidRemoveSize) {
		cout << x << ' ';
	}

	return 0;
}

// READ
// THINK
// CODE
// DEBUG
// AC
