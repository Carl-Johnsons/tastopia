import { useState, useRef } from "react";
import { View, Text, Animated, Pressable } from "react-native";
import { DownvoteIcon, UpvoteIcon } from "./SVG";
import { globalStyles } from "./GlobalStyles";
import { ROLE } from "@/slices/auth.slice";
import useProtected, { useProtectedExclude } from "@/hooks/auth/useProtected";

type VoteProps = {
  voteDiff: number;
};

const Vote = ({ voteDiff }: VoteProps) => {
  const [votes, setVotes] = useState(voteDiff);
  const [upvoted, setUpvoted] = useState(false);
  const [downvoted, setDownvoted] = useState(false);

  const upvoteBounceValue = useRef(new Animated.Value(1)).current;
  const downvoteBounceValue = useRef(new Animated.Value(1)).current;

  const handleUpvote = useProtectedExclude(() => {
    let newVotes = votes;
    if (upvoted) {
      newVotes -= 1;
      setUpvoted(false);
    } else {
      newVotes += 1;
      if (downvoted) {
        newVotes += 1;
        setDownvoted(false);
      }
      setUpvoted(true);
      bounceAnimation(upvoteBounceValue);
    }
    setVotes(newVotes);
  }, [ROLE.GUEST]);

  const handleDownvote = useProtected(() => {
    let newVotes = votes;
    if (downvoted) {
      newVotes += 1;
      setDownvoted(false);
    } else {
      newVotes -= 1;
      if (upvoted) {
        newVotes -= 1;
        setUpvoted(false);
      }
      setDownvoted(true);
      bounceAnimation(downvoteBounceValue);
    }
    setVotes(newVotes);
  }, [ROLE.USER]);

  const bounceAnimation = (value: any) => {
    Animated.sequence([
      Animated.timing(value, { toValue: 1.2, duration: 100, useNativeDriver: true }),
      Animated.spring(value, {
        toValue: 1,
        friction: 3,
        tension: 40,
        useNativeDriver: true
      })
    ]).start();
  };

  const getDigitCount = (votes: number) => {
    return votes.toString().length * 9;
  };

  return (
    <View className='rounded-3xl border-[0.5px] border-gray-300'>
      <View className='flex flex-row items-center justify-center'>
        <Pressable
          onPress={handleUpvote}
          style={{ borderTopLeftRadius: 24, borderBottomLeftRadius: 24 }}
        >
          <View className='flex-center flex-row py-2 pl-2'>
            <Animated.View
              style={{
                transform: [{ scale: upvoteBounceValue }],
                alignItems: "center",
                display: "flex",
                flexDirection: "row"
              }}
            >
              <UpvoteIcon
                color={upvoted ? globalStyles.color.primary : globalStyles.color.dark}
              />
            </Animated.View>
            <Text
              className={`mx-2 text-center ${upvoted || downvoted ? "color-primary" : "color-black"}`}
              style={{ width: getDigitCount(votes) }}
            >
              {isNaN(votes) ? "" : votes}
            </Text>
            <View className='h-full w-[1px] bg-gray-300' />
          </View>
        </Pressable>

        <Pressable
          onPress={handleDownvote}
          style={{ borderTopRightRadius: 24, borderBottomRightRadius: 24 }}
        >
          <View className='flex-center flex-row px-2 py-2'>
            <Animated.View
              style={{
                transform: [{ scale: downvoteBounceValue }],
                gap: 1,
                alignItems: "center",
                display: "flex",
                flexDirection: "row"
              }}
            >
              <DownvoteIcon
                color={downvoted ? globalStyles.color.primary : globalStyles.color.dark}
              />
            </Animated.View>
          </View>
        </Pressable>
      </View>
    </View>
  );
};

export default Vote;
