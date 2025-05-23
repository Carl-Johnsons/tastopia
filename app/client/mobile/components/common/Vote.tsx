import { useState, useRef, useEffect } from "react";
import { View, Text, Animated, Pressable, Alert } from "react-native";
import { globalStyles } from "./GlobalStyles";
import { ROLE } from "@/slices/auth.slice";
import useProtected, { useProtectedExclude } from "@/hooks/auth/useProtected";
import { AntDesign } from "@expo/vector-icons";
import useColorizer from "@/hooks/useColorizer";
import { colors } from "@/constants/colors";
import { VoteType } from "@/constants/recipe";
import { useVoteRecipe } from "@/api/recipe";
import { useErrorHandler } from "@/hooks/useErrorHandler";

type VoteProps = {
  vote: VoteType;
  voteDiff: number;
  recipeId: string;
};

const Vote = ({ vote, voteDiff, recipeId }: VoteProps) => {
  const { mutateAsync: voteMutation, isLoading: isVoting } = useVoteRecipe();
  const [votes, setVotes] = useState(voteDiff);
  const [upvoted, setUpvoted] = useState(vote === VoteType.UPVOTE);
  const [downvoted, setDownvoted] = useState(vote === VoteType.DOWNVOTE);
  const { black, white, primary } = colors;
  const { handleError } = useErrorHandler();
  const { c } = useColorizer();

  const upvoteBounceValue = useRef(new Animated.Value(1)).current;
  const downvoteBounceValue = useRef(new Animated.Value(1)).current;

  const handleUpvote = useProtectedExclude(() => {
    if (!isVoting) {
      voteMutation(
        { recipeId: recipeId, isUpvote: true },
        {
          onSuccess: async data => {
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
          },
          onError: error => handleError(error)
        }
      );
    }
  }, [ROLE.GUEST]);

  const handleDownvote = useProtected(() => {
    if (!isVoting) {
      voteMutation(
        { recipeId: recipeId, isUpvote: false },
        {
          onSuccess: async data => {
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
          },
          onError: error => handleError(error)
        }
      );
    }
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

  const upvoteForegound = c(
    upvoted ? primary : black.DEFAULT,
    upvoted ? primary : white.DEFAULT
  );
  const downvoteForegound = c(
    downvoted ? globalStyles.color.primary : globalStyles.color.dark,
    downvoted ? globalStyles.color.primary : globalStyles.color.light
  );

  useEffect(() => {
    setVotes(voteDiff);
    setUpvoted(vote === VoteType.UPVOTE);
    setDownvoted(vote === VoteType.DOWNVOTE);
  }, [vote, voteDiff, recipeId]);

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
              {upvoted ? (
                <AntDesign
                  name='like1'
                  size={16}
                  color={upvoteForegound}
                />
              ) : (
                <AntDesign
                  name='like2'
                  size={16}
                  color={upvoteForegound}
                />
              )}
            </Animated.View>
            <Text
              className={`text-black_white mx-2 text-center`}
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
              {downvoted ? (
                <AntDesign
                  name='dislike1'
                  size={16}
                  color={downvoteForegound}
                />
              ) : (
                <AntDesign
                  name='dislike2'
                  size={16}
                  color={downvoteForegound}
                />
              )}
            </Animated.View>
          </View>
        </Pressable>
      </View>
    </View>
  );
};

export default Vote;
