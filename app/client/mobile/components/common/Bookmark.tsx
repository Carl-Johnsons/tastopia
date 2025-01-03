import { StyleSheet, TouchableWithoutFeedback } from "react-native";
import React from "react";
import { BookmarkIcon, BookmarkedIcon } from "./SVG";

type BookMarkProps = {
  isBookmarked: boolean;
  handleToggleBookmark: () => void;
};

const Bookmark = ({ isBookmarked, handleToggleBookmark }: BookMarkProps) => {
  return (
    <TouchableWithoutFeedback onPress={handleToggleBookmark}>
      {isBookmarked ? <BookmarkedIcon /> : <BookmarkIcon />}
    </TouchableWithoutFeedback>
  );
};

export default Bookmark;
