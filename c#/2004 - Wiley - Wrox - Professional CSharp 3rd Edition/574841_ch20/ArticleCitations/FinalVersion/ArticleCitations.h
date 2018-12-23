#include <string>
using std::string;

class ArticleCitations
{
 public:
  ArticleCitations(const string& fileName);
  ~ArticleCitations();
  ArticleCitations(const ArticleCitations& src);
  ArticleCitations& operator=(const ArticleCitations& rhs);

  string getArticle() const { return mArticle; }
  int getNumCitations() const { return mNumCitations; }
  string getCitation(int i) const { return mCitations[i]; }

 protected:
  void readFile(const string& fileName);

  string mArticle;
  string* mCitations;
  int mNumCitations;
};
