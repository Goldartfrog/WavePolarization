# Getting Starting
To get started please first make an account on [http://remote2.ece.illinois.edu:3000/](http://) and then post your username on Slack.

# Installing Git and Git LFS

### Windows
#### Git
##### Method 1:
Download the latest version over here https://git-scm.com/downloads and run the installer.
##### Method 2:
If you have Windows Subsystem for Linux (WSL) installed, you can open WSL and follow the linux instructions.
#### Git LFS
Instructions and a download link can be found here https://git-lfs.github.com/.


### Mac
#### Git
To install git on Mac, open up a terminal window and type in `git --version'. If git is not installed, a prompt will show up.
#### Git-LFS
Run either `brew install git-lfs` or `port install git-lfs`, depending on if you have Homebrew or MacPorts.


### Linux
#### Debian
Run `sudo apt-get install -y git git-lfs`
#### CentOS
Run `sudo yum install git git-lfs`

# Getting started with git

First verify that git is install properly by running `git --version`. You should see it output something along the lines of "git version 2.11.0" if all went well. Next verify that git lfs is installed properly by running `git lfs --version`. You should see it output something along the lines of "git-lfs/2.7.1 (GitHub; linux amd64; go 1.11.5)"
### Cloning the repository
To clone the repository, run `git clone http://remote2.ece.illinois.edu:3000/PershingSquare/ece329vr-lab.git`.

### Making changes
Now make some changes and run `git add FILENAME` followed by `git commit`. Furthermore you can also run `git rm FILENANE` to remove a file, `git mv old_directory/FILENAME new_directory/TARGETFILE` to move a file.

### Creating a new branch
Merging directly into the master branch is forbidden and so you will have to create a new branch. To do so run `git checkout -b NEW_BRANCH_NAME` following by `git branch --set-upstream origin NEW_BRANCH_NAME`. Once you have created a new branch, you are free to push as often as you want onto this branch.

### Pushing your changes onto git
To push your changes onto git, run `git push`

And there you go you have succesfully pushed some changes using git!

# List of useful commands
To see what version of git you have installed `git --version`

For help with git `git help`

To initialize a new repo `git init`

To view current changes `git status`

### File operations

To add everything in the current directory `git add .`

To add a specific file `git add FILENAME`

To remove a specific file `git mv old/FILENAME new/FILENAME`

To remove a specific file `git rm FILENAME`

To remove a specific folder `git rm -r FILENAME`

To tell git to stop tracking a file `git rm --cached FILENAME`

### Commit Changes

To commit changes `git commit`

To commit changes with a message `git commit -m "MESSAGE"`

To add all changes and commit `git commit -a`

To add all changes and add a message `git commit -am "MESSAGE"`

To view all files with changes `git diff`

### Save Incomplete changes

Store incomplete changes `git stash`

Store all modified tracked files `git stash pop`

Restore most recently shashed files `git stash list`

### Branch operations

To create a new branch `git checkout -b branch_name`

To delete a branch `git branch -d branch_name`

To delete a branch that hasn't been merged `git branch -D branch_name`

To see commits `git log`

To undo all commits that you made up to COMMIT_HASH `git reset COMMIT_HASH`, note that that your changes in your local directory will stil be there.

To undo all discard your local changes run `git reset COMMIT_HASH --hard`

To upload all local branches `git pull`

To view the differences between two branches, `git diff FIRST_BRANCH SECOND_BRANCH`

To merge your local branch with another branch `git merge REMOTE/BRANCH`

### 

# Known Issues with git
### ssh doesn't work
ssh hasn't been setup